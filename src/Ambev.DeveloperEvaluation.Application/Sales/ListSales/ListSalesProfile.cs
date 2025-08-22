using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// AutoMapper profile for ListSales operation mappings
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales operation
    /// </summary>
    public ListSalesProfile()
    {
        // Entity to Result mappings
        CreateMap<Sale, SaleListItem>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}