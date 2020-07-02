using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using AutoMapper;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.ServiceAgents;
using ParcelLogistics.SKS.Package.ServiceAgents.Interfaces;

namespace ParcelLogistics.SKS.Package.BusinessLogic
{
    public class TrackingLogic : ITrackingLogic
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly IGeoEncodingAgent _encoder;

        private static readonly Random Random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public TrackingLogic(IParcelRepository parcelRepository, IWarehouseRepository warehouseRepository, IMapper mapper, IGeoEncodingAgent encoder)
        {
            _parcelRepository = parcelRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _encoder = encoder;
        }

        private List<DataAccess.Entities.Hop> GetNextHopsToTarget(Dictionary<string, DataAccess.Entities.Warehouse> hops, string source, string target)
        {
            var list = new List<DataAccess.Entities.Hop>();
            DataAccess.Entities.Warehouse from = hops[source];

            foreach (DataAccess.Entities.WarehouseNextHops next in from.NextHops)
            {
                if (next.Hop.HopType != "Warehouse")
                    continue; 

                if (next.Hop.Code == target)
                {
                    list.Add(hops[target]);
                    return list;
                }
                else
                {
                    list.AddRange(GetNextHopsToTarget(hops, next.Hop.Code, target));
                    if (list.Count > 0)
                    {
                        list.Add(hops[next.Hop.Code]);
                        return list;
                    }
                }
            }
            return list;
        }

        private List<HopArrival> PredictFutureHops(Parcel parcel)
        {
            var validator = new RecipientValidator();
            var checkRecipient = validator.Validate(parcel.Receipient);
            var checkSender = validator.Validate(parcel.Sender);

            //Recipient validation
            if (!checkRecipient.IsValid)
            {
                throw new BlException("Invalid Recipient Adress");
            }

            //Sender validation
            if (!checkSender.IsValid)
            {
                throw new BlException("Invalid Sender Adress");
            }

            // predict future hops to final destination
            var senderLocation = _encoder.EncodeAddress($"{parcel.Sender.Street}, {parcel.Sender.PostalCode} {parcel.Sender.City}, {parcel.Sender.Country}");
            var receiverLocation = _encoder.EncodeAddress($"{parcel.Receipient.Street}, {parcel.Receipient.PostalCode} {parcel.Receipient.City}, {parcel.Receipient.Country}");

            if(senderLocation == null || receiverLocation == null)
            {
                throw new BlException("Receiver and Sender adress unknown.");
            }

            // find trucks in selected range:
            var senderTruck = (DataAccess.Entities.Truck)_warehouseRepository.GetByCoordinates(senderLocation.Longitude, senderLocation.Latitude);
            var receiverTruck = (DataAccess.Entities.Truck)_warehouseRepository.GetByCoordinates(receiverLocation.Longitude, receiverLocation.Latitude);

            if (senderTruck == null || receiverTruck == null)
            {
                throw new BlException("There is no truck close to this area!");
            }

            //All warehouses
            Dictionary<string, DataAccess.Entities.Warehouse> dicHop = new Dictionary<string, DataAccess.Entities.Warehouse>();
            var allWh = _warehouseRepository.GetAllWarehouses();
            var rootWh = allWh.FirstOrDefault();
            foreach (DataAccess.Entities.Warehouse wh in allWh)
            {
                if (wh.ID == 1)
                    rootWh = wh;
                dicHop.Add(wh.Code, wh);
            }

            //warehouse for the truck (for sender and receipient).
            var senderWh = allWh.Where(h => h.NextHops.Any(nh => nh.Hop.Code == senderTruck.Code)).Single();
            var receiverWh = allWh.Where(h => h.NextHops.Any(nh => nh.Hop.Code == receiverTruck.Code)).Single();

            //Add first hop arrival to the future hops
            parcel.FutureHops = new List<HopArrival>();
            DateTime dateTime = DateTime.Now.AddMinutes((double)senderTruck.ProcessingDelayMins);
            parcel.FutureHops.Add(new HopArrival() { Code = senderTruck.Code, DateTime = dateTime });

            if (senderWh.Code != receiverWh.Code)
            {
                //all hops from root to the goal destination(warehouse)
                var hopsSender = GetNextHopsToTarget(dicHop, rootWh.Code, senderWh.Code);
                hopsSender.Reverse();
                var hopsReceiver = GetNextHopsToTarget(dicHop, rootWh.Code, receiverWh.Code);

                // if no Intersections rootWh is only intersection
                var firstIntersect = hopsSender.Intersect(hopsReceiver).FirstOrDefault();
                if (null == firstIntersect)
                {
                    firstIntersect = rootWh;
                }

                //save all hops while there is no intersecting
                var hopsList = hopsSender.TakeWhile(h => h != firstIntersect).ToList();
                hopsList.Add(firstIntersect);
                hopsList.AddRange(hopsReceiver.AsEnumerable().Reverse().TakeWhile(h => h != firstIntersect));

                foreach (DataAccess.Entities.Hop h in hopsList)
                {
                    dateTime = dateTime.AddMinutes((double)h.ProcessingDelayMins);
                    parcel.FutureHops.Add(new HopArrival() { Code = h.Code, DateTime = dateTime });
                }
            }
            dateTime = dateTime.AddMinutes((double)receiverTruck.ProcessingDelayMins);
            parcel.FutureHops.Add(new HopArrival() { Code = receiverTruck.Code, DateTime = dateTime });

            return parcel.FutureHops;    
        }

        public Parcel submitParcel(Parcel parcel)
        {

            var validator = new ParcelValidator();
            var recvalidator = new RecipientValidator();

            var checkParcel = validator.Validate(parcel);
            var checRecipient = recvalidator.Validate(parcel.Receipient);
            var checSender = recvalidator.Validate(parcel.Sender);

            if (!checkParcel.IsValid)
            {
                throw new BlException("Parcel weight must be greater than zero.");
            }

            if (!checRecipient.IsValid)
            {
                throw new BlException("Recipient is not valid");
            }

            if (!checSender.IsValid)
            {
                throw new BlException("Sender is not valid");
            }

            if (parcel.TrackingId == null)
            {
                var trackingId = new string(Enumerable.Repeat(Chars, 9).Select(s => s[Random.Next(s.Length)]).ToArray());
                parcel.TrackingId = trackingId;
            }
        
            parcel.FutureHops = PredictFutureHops(parcel);
            _parcelRepository.Create(_mapper.Map<DataAccess.Entities.Parcel>(parcel));
            return new Parcel() { TrackingId = parcel.TrackingId };
        }

        public Parcel trackParcel(string TrackingId)
        {
            if(TrackingId == null){
                throw new BlException("Id can not be null.");
            }

            DataAccess.Entities.Parcel parcel = _parcelRepository.GetByTrackingId(TrackingId);
            Parcel p = _mapper.Map<Parcel>(parcel);
            return p;
        }

        public void transitionParcel(string TrackingId, string code)
        {
            var parcel = _parcelRepository.GetByTrackingId(TrackingId);
            DataAccess.Entities.Hop hop = _warehouseRepository.GetByCode(code);

            List<DataAccess.Entities.HopArrival> visited = new List<DataAccess.Entities.HopArrival>();
            foreach (DataAccess.Entities.HopArrival h in parcel.FutureHops)
            {
                visited.Add(h);
                h.TimeStamp = DateTime.Now;
                parcel.VisitedHops.Add(h);
            }

            foreach (DataAccess.Entities.HopArrival h in visited)
            {
                parcel.FutureHops.Remove(h);
            }

            if (hop.HopType == "TransferWarehouse") // delivery to other logistics center
            {
                var transfer = (DataAccess.Entities.Transferwarehouse)hop;
                using (var client = new HttpClient())
                {
                    string parcelJson = Newtonsoft.Json.JsonConvert.SerializeObject(parcel);
                    var response = client.PostAsync(transfer.LogisticsPartnerUrl + "/parcel", new StringContent(parcelJson, Encoding.UTF8, "application/json"));
                }
                parcel.State = DataAccess.Entities.Parcel.StateEnum.DeliveredEnum;
            }
            else if (hop.HopType == "Truck") // delivery to customer per truck
            {
                parcel.State = DataAccess.Entities.Parcel.StateEnum.InTruckDeliveryEnum;
            }
            else
            {
                parcel.State = DataAccess.Entities.Parcel.StateEnum.InTransportEnum;
            }
            _parcelRepository.Update(parcel);
        }

        public void ReportParcelDelivery(string trackingId)
        {
            try
            {
                var parcel = _parcelRepository.GetByTrackingId(trackingId);

                foreach (DataAccess.Entities.HopArrival h in parcel.FutureHops)
                {
                    h.TimeStamp = DateTime.Now;
                    parcel.VisitedHops.Add(h);
                }
                parcel.FutureHops.Clear();
                parcel.State = DataAccess.Entities.Parcel.StateEnum.DeliveredEnum;
                _parcelRepository.Update(parcel);
            }
            catch
            {
                throw new BlException("There was an error by reporting delivery");
            }
        }

        public Parcel TrackParcel(string trackingId)
        {
            if(trackingId == null)
            {
                throw new BlException("Tracking id should not be empty!");
            }
            
            var parcel = _parcelRepository.GetByTrackingId(trackingId);
                var p = _mapper.Map<Parcel>(parcel);
                return p;
        }
    }
}
