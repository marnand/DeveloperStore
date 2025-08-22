using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale entity in the system.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the associated sale
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Date of sale
    /// </summary>
    public DateTime SaleDate { get; set; }
    /// <summary>
    /// Gets or sets the identifier of the associated customer
    /// </summary>
    public Guid CustomerId { get; set; }
    /// <summary>
    /// Gets or sets the customer associated
    /// </summary>
    public Customer? Customer { get; set; }
    /// <summary>
    /// Gets or sets the identifier of the associated subsidiary
    /// </summary>
    public Guid SubsidiaryId { get; set; }
    /// <summary>
    /// Gets or sets the subsidiary associated
    /// </summary>
    public Subsidiary? Subsidiary { get; set; }
    /// <summary>
    /// Gets or sets the status of sale
    /// </summary>
    public SaleStatus Status { get; set; }
    /// <summary>
    /// Gets or sets the list of SaleItem
    /// </summary>
    public List<SaleItem> Items { get; set; } = [];
    /// <summary>
    /// Gets or sets the total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the subsidiary was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the subsidiary was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        SaleDate = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Pending;
        SaleNumber = GenerateSaleNumber();
    }

    /// <summary>
    /// Calculates the total amount of the sale by summing the totals of each item.
    /// </summary>
    /// <returns>Decimal value</returns>
    public decimal CalculateTotal()
    {
        if (Status == SaleStatus.Cancelled)
            return 0;

        return Items.Sum(item => item.CalculateTotal());
    }

    /// <summary>
    /// Adds a new item to the sale.
    /// </summary>
    /// <param name="item">The sales item to add</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to add items to a cancelled or completed sale</exception>
    public void AddItem(SaleItem item)
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("You cannot add items to a canceled sale.");
        
        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException("You cannot add items to a completed sale.");

        item.SaleId = Id;
        item.ApplyDiscountRules();
        Items.Add(item);
        TotalAmount = CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the sale, setting its status to Cancelled and updating the timestamp.
    /// </summary>
    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
        TotalAmount = CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels a specific item in the sale by setting its IsCancelled property to true and updating the timestamp.
    /// </summary>
    /// <param name="itemId">The ID of the item to cancel</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to cancel items in a cancelled sale</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the item is not found</exception>
    public void CancelItem(Guid itemId)
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Cannot cancel items in a cancelled sale.");
            
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new KeyNotFoundException($"Item with ID {itemId} not found in this sale.");
            
        if (item.IsCancelled)
            throw new InvalidOperationException("Item is already cancelled.");

        item.Cancel();
        TotalAmount = CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Completes the sale, setting its status to Completed and updating the timestamp.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when trying to complete a cancelled sale or a sale with no items</exception>
    public void Complete()
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled sale.");
            
        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException("Sale is already completed.");
            
        if (!Items.Any() || Items.All(i => i.IsCancelled))
            throw new InvalidOperationException("Cannot complete a sale with no active items.");

        Status = SaleStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Updates an existing item in the sale
    /// </summary>
    /// <param name="itemId">The ID of the item to update</param>
    /// <param name="quantity">New quantity</param>
    /// <param name="unitPrice">New unit price</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to update items in a completed or cancelled sale</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the item is not found</exception>
    public void UpdateItem(Guid itemId, int quantity, decimal unitPrice)
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Cannot update items in a cancelled sale.");
            
        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException("Cannot update items in a completed sale.");
            
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new KeyNotFoundException($"Item with ID {itemId} not found in this sale.");
            
        if (item.IsCancelled)
            throw new InvalidOperationException("Cannot update a cancelled item.");

        item.Quantity = quantity;
        item.UnitPrice = unitPrice;
        item.ApplyDiscountRules();
        UpdatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Removes an item from the sale
    /// </summary>
    /// <param name="itemId">The ID of the item to remove</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to remove items from a completed or cancelled sale</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the item is not found</exception>
    public void RemoveItem(Guid itemId)
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Cannot remove items from a cancelled sale.");
            
        if (Status == SaleStatus.Completed)
            throw new InvalidOperationException("Cannot remove items from a completed sale.");
            
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new KeyNotFoundException($"Item with ID {itemId} not found in this sale.");

        Items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Generates a unique sale number based on the current date and time, followed by a random number.
    /// </summary>
    /// <returns>A unique sale number string</returns>
    private string GenerateSaleNumber()
    {
        var random = new Random();
        var randomPart = random.Next(1000, 9999).ToString();
        return $"{DateTime.UtcNow:yyyyMMddHHmmss}{randomPart}";
    }
    
    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Sale number format and length</list>
    /// <list type="bullet">Sale date validity</list>
    /// <list type="bullet">Customer and Subsidiary IDs</list>
    /// <list type="bullet">Items collection and individual item validation</list>
    /// </remarks>
    public ValidationResultDetail Validation()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
