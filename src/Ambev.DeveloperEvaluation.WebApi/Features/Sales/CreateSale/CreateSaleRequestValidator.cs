using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleRequest
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.SubsidiaryId)
            .NotEmpty()
            .WithMessage("Subsidiary ID is required");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required")
            .Must(items => items.Count > 0)
            .WithMessage("At least one item is required");

        RuleForEach(x => x.Items)
            .SetValidator(new CreateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for CreateSaleItemRequest
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleItemRequest
    /// </summary>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity cannot exceed 20 units per item");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than 0");
    }
}