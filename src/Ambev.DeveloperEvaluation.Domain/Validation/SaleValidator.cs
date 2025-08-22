using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for Sale entity that defines validation rules for sale properties.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of SaleValidator with validation rules.
    /// </summary>
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .Length(1, 50).WithMessage("Sale number must be between 1 and 50 characters.");

        RuleFor(sale => sale.SaleDate)
            .NotEmpty().WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(sale => sale.SubsidiaryId)
            .NotEmpty().WithMessage("Subsidiary ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.")
            .Must(items => items.Any(item => !item.IsCancelled))
            .WithMessage("Sale must have at least one active (non-cancelled) item.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}