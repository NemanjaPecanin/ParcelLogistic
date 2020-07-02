using FluentValidation;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators
{
    public class HopArrivalValidator : AbstractValidator<Warehouse>
    {
        private bool ValidHopType(string val)
        {
            return val.Equals("Truck") || val.Equals("Warehouse") || val.Equals("Transferwarehouse");
        }

        public HopArrivalValidator()
        {
            RuleFor(parcel => parcel.Code).NotEmpty();
            RuleFor(x => x.HopType).NotNull();
            RuleFor(x => x.HopType).Must(ValidHopType).When(x => !string.IsNullOrEmpty(x.HopType));
        }
    }
}
