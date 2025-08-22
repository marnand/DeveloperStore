using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for updating sales.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of UpdateSaleCommandValidator with validation rules.
    /// </summary>
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        RuleFor(x => x.SubsidiaryId)
            .NotEmpty()
            .WithMessage("Subsidiary ID is required.");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item.");

        RuleForEach(x => x.Items)
            .SetValidator(new UpdateSaleItemCommandValidator());
    }
}

/// <summary>
/// Validator for UpdateSaleItemCommand that defines validation rules for updating sale items.
/// </summary>
public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of UpdateSaleItemCommandValidator with validation rules.
    /// </summary>
    public UpdateSaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity cannot exceed 20 items.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");
    }
}