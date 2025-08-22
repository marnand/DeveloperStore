using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Validator for ListSalesRequest
/// </summary>
public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    private readonly string[] _validSortFields = { "SaleDate", "SaleNumber", "TotalAmount", "Status", "CreatedAt" };
    private readonly string[] _validSortDirections = { "asc", "desc" };
    private readonly string[] _validStatuses = { "Pending", "Completed", "Cancelled" };

    /// <summary>
    /// Initializes validation rules for ListSalesRequest
    /// </summary>
    public ListSalesRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");

        RuleFor(x => x.Size)
            .GreaterThan(0)
            .WithMessage("Size must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Size cannot exceed 100");

        RuleFor(x => x.SortDirection)
            .Must(direction => _validSortDirections.Contains(direction.ToLower()))
            .WithMessage("Sort direction must be 'asc' or 'desc'");

        RuleFor(x => x.SortBy)
            .Must(sortBy => _validSortFields.Contains(sortBy))
            .WithMessage($"Sort by must be one of: {string.Join(", ", _validSortFields)}");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("Start date must be less than or equal to end date");

        RuleFor(x => x.Status)
            .Must(status => string.IsNullOrEmpty(status) || _validStatuses.Contains(status))
            .WithMessage($"Status must be one of: {string.Join(", ", _validStatuses)}");
    }
}