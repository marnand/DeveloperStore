using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ISubsidiaryRepository _subsidiaryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        ICustomerRepository customerRepository,
        ISubsidiaryRepository subsidiaryRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _customerRepository = customerRepository;
        _subsidiaryRepository = subsidiaryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Get existing sale
        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        // Validate customer exists
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with Id {command.CustomerId} not found");

        // Validate subsidiary exists
        var subsidiary = await _subsidiaryRepository.GetByIdAsync(command.SubsidiaryId, cancellationToken);
        if (subsidiary == null)
            throw new KeyNotFoundException($"Subsidiary with ID {command.SubsidiaryId} not found");

        // Update basic sale properties
        existingSale.CustomerId = command.CustomerId;
        existingSale.SubsidiaryId = command.SubsidiaryId;

        // Handle items updates
        var existingItemIds = existingSale.Items.Select(i => i.Id).ToHashSet();
        var commandItemIds = command.Items.Where(i => i.Id.HasValue).Select(i => i.Id!.Value).ToHashSet();

        // Remove items that are no longer in the command
        var itemsToRemove = existingSale.Items.Where(i => !commandItemIds.Contains(i.Id)).ToList();
        foreach (var item in itemsToRemove)
        {
            existingSale.RemoveItem(item.Id);
        }

        // Update or add items
        foreach (var itemCommand in command.Items)
        {
            // Validate product exists
            var product = await _productRepository.GetByIdAsync(itemCommand.ProductId, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {itemCommand.ProductId} not found");

            if (itemCommand.Id.HasValue && existingItemIds.Contains(itemCommand.Id.Value))
            {
                // Update existing item
                existingSale.UpdateItem(itemCommand.Id.Value, itemCommand.Quantity, itemCommand.UnitPrice);
            }
            else
            {
                // Add new item
                var newItem = new SaleItem
                {
                    ProductId = itemCommand.ProductId,
                    Quantity = itemCommand.Quantity,
                    UnitPrice = itemCommand.UnitPrice
                };
                existingSale.AddItem(newItem);
            }
        }

        // Save the updated sale
        var updatedSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);

        // Publish domain event
        var saleModifiedEvent = new SaleModifiedEvent(updatedSale);
        // Note: Event publishing would be handled by domain event dispatcher

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}