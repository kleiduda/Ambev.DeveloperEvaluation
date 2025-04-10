using Ambev.DeveloperEvaluation.Application.Sales.Dto;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src));

            CreateMap<SaleItem, SaleItemDto>();
        }
    }

}
