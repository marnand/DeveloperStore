using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Handler for processing ListSalesCommand requests
/// </summary>
public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListSalesHandler
    /// </summary>
    public ListSalesHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSalesCommand request
    /// </summary>
    public async Task<ListSalesResult> Handle(ListSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Parse status enum if provided
        SaleStatus? statusFilter = null;
        if (!string.IsNullOrEmpty(command.Status))
        {
            if (Enum.TryParse<SaleStatus>(command.Status, true, out var parsedStatus))
            {
                statusFilter = parsedStatus;
            }
        }

        // Get paginated sales with filters
        var (sales, totalCount) = await _saleRepository.GetPaginatedAsync(
            page: command.Page,
            size: command.Size,
            customerId: command.CustomerId,
            subsidiaryId: command.SubsidiaryId,
            status: statusFilter,
            startDate: command.StartDate,
            endDate: command.EndDate,
            sortBy: command.SortBy ?? "SaleDate",
            sortDirection: command.SortDirection,
            cancellationToken: cancellationToken);

        // Calculate pagination metadata
        var totalPages = (int)Math.Ceiling((double)totalCount / command.Size);
        var hasNext = command.Page < totalPages;
        var hasPrevious = command.Page > 1;

        // Map to result DTOs
        var saleListItems = _mapper.Map<List<SaleListItem>>(sales);

        return new ListSalesResult
        {
            Sales = saleListItems,
            CurrentPage = command.Page,
            TotalPages = totalPages,
            PageSize = command.Size,
            TotalCount = totalCount,
            HasNext = hasNext,
            HasPrevious = hasPrevious
        };
    }
}