using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ISubsidiaryRepository _subsidiaryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    public CreateSaleHandler(
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
    /// Handles the CreateSaleCommand request
    /// </summary>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with Id {command.CustomerId} not found");

        var Subsidiary = await _subsidiaryRepository.GetByIdAsync(command.SubsidiaryId, cancellationToken);
        if (Subsidiary == null)
            throw new KeyNotFoundException($"Subsidiary with ID {command.SubsidiaryId} not found");

        var saleItems = new List<SaleItem>();
        foreach (var itemCommand in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemCommand.ProductId, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {itemCommand.ProductId} not found");

            var saleItem = new SaleItem
            {
                ProductId = itemCommand.ProductId,
                Quantity = itemCommand.Quantity,
                UnitPrice = itemCommand.UnitPrice
            };
            saleItems.Add(saleItem);
        }

        var sale = new Sale
        {
            CustomerId = command.CustomerId,
            SubsidiaryId = command.SubsidiaryId,
            Items = saleItems
        };

        // Apply business rules (discounts)
        // sale.ApplyDiscountRules();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        // Publish domain event
        var saleCreatedEvent = new SaleCreatedEvent(createdSale);
        // Note: Event publishing would be handled by domain event dispatcher

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}