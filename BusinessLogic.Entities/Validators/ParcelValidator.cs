using FluentValidation;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators
{
    public class ParcelValidator : AbstractValidator<Parcel>
    {
        public ParcelValidator()
        {
            //weight greater than 0
            RuleFor(x => x.Weight).GreaterThan(0.0f);
            //9 numbers or letters (uppercase)
            RuleFor(x => x.TrackingId).Matches("^[A-Z0-9]{9}$");
        }
    }
}
