using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SalesItemValidator : AbstractValidator<SalesItem>
{
    public SalesItemValidator()
    {
        RuleFor(item => item.Quantity)
            .InclusiveBetween(0, 20).WithMessage("Quantity must be between 0 end 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        
        RuleFor(item => item.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");
    }
}
