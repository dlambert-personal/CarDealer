using FluentValidation;
using System.Runtime.CompilerServices;

namespace DealerService.Models
{
    public class ModelValidationResult<M, V> where V : AbstractValidator<M>, new()
    {
        public ModelValidationResult(M model)
        {
            this.Model = model; 
        }
        public M Model { get; private set; }
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }
        public bool Validate()
        {
            V validator = new V();
            this.ValidationResult = validator.Validate(this.Model);
            return ValidationResult.IsValid;
        }
    }
}
