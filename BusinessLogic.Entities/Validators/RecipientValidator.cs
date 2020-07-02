using FluentValidation;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators
{
    public class RecipientValidator : AbstractValidator<Receipient>
    {
        public RecipientValidator()
        {
            RuleFor(x => x.Name).Matches(@"[A-Z]{1}[A-Za-z -]");
            RuleFor(x => x.Street).Matches(@"^([A-Z]{1}[a-zß]+)+(\s{1}[0-9A-zß/]+)*$");
            RuleFor(x => x.PostalCode).Matches(@"^A-\d{4}");
            RuleFor(x => x.City).Matches(@"[A-Z][A-Za-z -]");
            RuleFor(x => x.Country).Matches(@"[A-Z][A-Za-z -]");
        }
    }
}
