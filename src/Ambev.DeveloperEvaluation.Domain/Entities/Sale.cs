using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

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
    /// Gets or sets the list of SalesItem
    /// </summary>
    public List<SalesItem> Items { get; set; } = [];
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
    /// <param name="item"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void AddItem(SalesItem item)
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("You cannot add items to a canceled sale.");

        item.ApplyDiscountRules();

        Items.Add(item);
    }

    /// <summary>
    /// Cancels the sale, setting its status to Cancelled and updating the timestamp.
    /// </summary>
    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels a specific item in the sale by setting its IsCancelled property to true and updating the timestamp.
    /// </summary>
    /// <param name="itemId"></param>
    public void CancelItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Completes the sale, setting its status to Completed and updating the timestamp.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Complete()
    {
        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Não é possível completar uma venda cancelada.");

        Status = SaleStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Generates a unique sale number based on the current date and time, followed by a random number.
    /// </summary>
    /// <returns></returns>
    private string GenerateSaleNumber()
    {
        var random = new Random();
        var randomPart = random.Next(1000, 9999).ToString();
        return $"{DateTime.UtcNow:yyyyMMddHHmmss}{randomPart}";
    }
}
