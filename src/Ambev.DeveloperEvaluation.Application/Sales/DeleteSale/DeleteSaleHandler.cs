using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler
    /// </summary>
    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Get existing sale
        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
        {
            return new DeleteSaleResult
            {
                Id = command.Id,
                Success = false,
                Message = $"Sale with ID {command.Id} not found"
            };
        }

        // Cancel the sale instead of hard delete (business rule)
        existingSale.Cancel();
        await _saleRepository.UpdateAsync(existingSale, cancellationToken);

        // Publish domain event
        var saleCancelledEvent = new SaleCancelledEvent(existingSale);
        // Note: Event publishing would be handled by domain event dispatcher

        return new DeleteSaleResult
        {
            Id = command.Id,
            Success = true,
            Message = "Sale cancelled successfully"
        };
    }
}