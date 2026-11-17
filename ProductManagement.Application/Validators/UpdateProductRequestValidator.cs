using FluentValidation;
using ProductManagement.Application.DTOs.Requests;

namespace ProductManagement.Application.Validators
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500)
                .WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");

            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage("Product category is required.")
                .MaximumLength(100)
                .WithMessage("Product category cannot exceed 100 characters.");
        }
    }
}
