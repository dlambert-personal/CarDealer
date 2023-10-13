using FluentValidation;

namespace DealerService.Models
{
    public class Dealer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Website {  get; set; }
    }

    public class DealerValidator:FluentValidation.AbstractValidator<Dealer>
    {
        public DealerValidator()
        {
            RuleFor(d => d.Website)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(d => !string.IsNullOrEmpty(d.Website))
                .WithMessage("Supplied value does not appear to be a valid web address.");
        }
    }
}
