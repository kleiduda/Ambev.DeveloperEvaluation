using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleResult, GetSaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Sale.Id))
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.Sale.SaleNumber))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Sale.SaleDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Sale.CustomerId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Sale.CustomerName))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Sale.BranchId))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Sale.BranchName))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Sale.TotalAmount))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.Sale.IsCancelled))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Sale.Items));

            CreateMap<SaleItem, GetSaleItemResponse>();
        }
    }


}
