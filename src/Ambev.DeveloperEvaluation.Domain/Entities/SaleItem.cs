using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sales transaction
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the associated sale
    /// </summary>
    public Guid SaleId { get; set; }
    /// <summary>
    /// Gets or sets the sale associated with this item
    /// </summary>
    public Sale? Sale { get; set; }
    /// <summary>
    /// Gets or sets the productId of associated sale
    /// </summary>
    public Guid ProductId { get; set; }
    /// <summary>
    /// Gets or sets the product associated with this item
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
    /// Gets or sets the total amount for this sale item
    /// </summary>
    public decimal TotalAmount { get; set; }

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
    /// Calculate discount percentage based on business rules:
    /// - 4-9 items: 10% discount
    /// - 10-20 items: 20% discount
    /// - Below 4 items: no discount
    /// - Above 20 items: not allowed
    /// </summary>
    /// <returns>
    /// Percentage value
    /// </returns>
    public decimal CalculateDiscountPercentage()
    {
        if (Quantity >= 4 && Quantity < 10) 
            return 10;

        if (Quantity >= 10 && Quantity <= 20) 
            return 20;

        return 0;
    }

    /// <summary>
    /// Applies discount rules according to business requirements
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when quantity exceeds 20 items</exception>
    public void ApplyDiscountRules()
    {
        if (Quantity > 20)
            throw new InvalidOperationException("It's not possible to sell above 20 identical items.");

        if (Quantity < 4)
        {
            DiscountPercentage = 0;
        }
        else
        {
            DiscountPercentage = CalculateDiscountPercentage();
        }
        
        // Update total amount after applying discount rules
        TotalAmount = CalculateTotal();
    }

    /// <summary>
    /// Cancels this sales item
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
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
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
