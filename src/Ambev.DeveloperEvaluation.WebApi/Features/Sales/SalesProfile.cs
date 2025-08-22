using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// AutoMapper profile for Sales feature mappings
/// </summary>
public class SalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for Sales feature
    /// </summary>
    public SalesProfile()
    {
        // CreateSale mappings
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();

        // GetSale mappings
        CreateMap<Guid, GetSaleCommand>()
            .ConstructUsing(id => new GetSaleCommand(id));
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();

        // UpdateSale mappings
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();

        // DeleteSale mappings
        CreateMap<Guid, DeleteSaleCommand>()
            .ConstructUsing(id => new DeleteSaleCommand(id));

        // ListSales mappings
        CreateMap<ListSalesRequest, ListSalesCommand>();
        CreateMap<Ambev.DeveloperEvaluation.Application.Sales.ListSales.SaleListItem, Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales.SaleListItem>();
    }
}