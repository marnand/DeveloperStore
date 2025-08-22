using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Validator for ListSalesCommand that defines validation rules for listing sales.
/// </summary>
public class ListSalesCommandValidator : AbstractValidator<ListSalesCommand>
{
    /// <summary>
    /// Initializes a new instance of ListSalesCommandValidator with validation rules.
    /// </summary>
    public ListSalesCommandValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0.");

        RuleFor(x => x.Size)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100.");

        RuleFor(x => x.SortDirection)
            .Must(x => string.IsNullOrEmpty(x) || x.ToLower() == "asc" || x.ToLower() == "desc")
            .WithMessage("Sort direction must be 'asc' or 'desc'.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("Start date must be less than or equal to end date.");

        RuleFor(x => x.Status)
            .Must(x => string.IsNullOrEmpty(x) || 
                      x.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("Completed", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Status must be 'Pending', 'Completed', or 'Cancelled'.");

        RuleFor(x => x.SortBy)
            .Must(x => string.IsNullOrEmpty(x) ||
                      x.Equals("SaleDate", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("SaleNumber", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("TotalAmount", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("Status", StringComparison.OrdinalIgnoreCase) ||
                      x.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
            .WithMessage("SortBy must be one of: SaleDate, SaleNumber, TotalAmount, Status, CreatedAt.");
    }
}