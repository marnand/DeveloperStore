using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sales transaction
/// </summary>
public class SalesItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the associated sale
    /// </summary>
    public Guid SaleId { get; set; }
    /// <summary>
    /// Gets or sets the product of the associated sale
    /// </summary>
    public Product? Product { get; set; }
    /// <summary>
    /// Gets or sets the quantity of sale item
    /// </summary>
    public int Quantity { get; set; }
    /// <summary>
    /// Gets or sets the unite price of sale item
    /// </summary>
    public decimal UnitPrice { get; set; }
    /// <summary>
    /// Gets or sets the discount percentage of sale item
    /// </summary>
    public decimal DiscountPercentage { get; set; }
    /// <summary>
    /// Gets or sets the cancelled status
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Calculate total sales item
    /// </summary>
    /// <returns>
    /// Decimal value
    /// </returns>
    public decimal CalculateTotal()
    {
        if (IsCancelled)
            return 0;

        var total = Quantity * UnitPrice;
        var discount = total * (DiscountPercentage / 100);
        return total - discount;
    }

    /// <summary>
    /// Calculate discount percentage
    /// </summary>
    /// <returns>
    /// Percetage value
    /// </returns>
    public decimal CalculateDiscountPercentage()
    {
        if (Quantity > 4 && Quantity < 10) 
            return 10;

        if (Quantity >= 10 && Quantity <= 20) 
            return 20;

        return 0;
    }

    public void ApplyDiscountRules()
    {
        if (Quantity > 20)
            throw new InvalidOperationException("Não é possível vender mais de 20 itens idênticos.");

        DiscountPercentage = CalculateDiscountPercentage();
    }

    /// <summary>
    /// Performs validation of the user entity using the SalesItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Quantity rage</list>
    /// <list type="bullet">UnitPrice greater than</list>
    /// <list type="bullet">DiscountPercentage rage</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validation()
    {
        var validator = new SalesItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
