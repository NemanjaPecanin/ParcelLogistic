using FluentValidation;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators
{
    public class WarehouseValidator : AbstractValidator<Warehouse>
    {
        private bool ValidHopType(string val)
        {
            return val.Equals("Truck") || val.Equals("Warehouse") || val.Equals("Transferwarehouse");
        }

        public WarehouseValidator()
        {
            RuleFor(x => x.Code).NotNull().Matches("^[A-Z]{4}\\d{1,4}$");
            RuleFor(x => x.Description).NotNull().Matches("^[A-zÄÖÜäöüß\\-\\s0-9]+$");
            RuleFor(x => x.HopType).NotNull();
            RuleFor(x => x.HopType).Must(ValidHopType).When(x => !string.IsNullOrEmpty(x.HopType));
        }
    }
}
