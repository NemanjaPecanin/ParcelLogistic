using AutoMapper;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using ParcelLogistics.SKS.Package.BusinessLogic.Tests;
using ParcelLogistics.SKS.Package.Services.DTOs;
using ParcelLogistics.SKS.Package.Services.Mapper;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ParcelLogistics.SKS.Package.IntegrationTests
{
    public class IntegrationTests
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        private string _trackingId = string.Empty;

        public IntegrationTests()
        {
            //Set Base of URL Adress
            var url = "http://localhost:50352/";
            Client.BaseAddress = new Uri(url);
            //Clear all accepted headers
            Client.DefaultRequestHeaders.Accept.Clear();
            //set json as accepted header
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Test, Order(1)]
        public void SubmitParcel()
        {
            var response = Client.PostAsJsonAsync("api/parcel",
                _mapper.Map<BusinessLogic.Entities.Parcel>(MockBuilder.NotNgBuilderParcel())).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            var info = response.Content.ReadAsAsync<NewParcelInfo>().Result;
            _trackingId = info.TrackingId;
        }

        [Test, Order(2)]
        public void TrackParcel_Succeeded()
        {

            var response = Client.GetAsync($"api/parcel/{_trackingId}").Result;
            Console.Write(response);
            Console.Write(_trackingId);
            Assert.IsTrue(response.IsSuccessStatusCode);           
        }

        [Test, Order(3)]
        public void ReportHop_Succeeded()
        {
            var response = Client.PostAsJsonAsync($"api/parcel/{_trackingId}/reportHop/AEUA01", "").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }


        [Test, Order(4)]
        public void ReportHop_Succeeded_secondTIme()
        {
            var response = Client.PostAsJsonAsync($"api/parcel/{_trackingId}/reportHop/HATA016", "").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test, Order(5)]
        public void TrackParcel_Succeeded_secondTime()
        {

            var response = Client.GetAsync($"api/parcel/{_trackingId}").Result;
            Console.Write(response);
            Console.Write(_trackingId);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test, Order(6)]
        public void ReportParcelDelivery_Succeeded()
        {
            var response = Client.PostAsJsonAsync($"api/parcel/{_trackingId}/reportDelivery/", "").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

    }
}
