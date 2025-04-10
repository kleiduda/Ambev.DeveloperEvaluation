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
                .ForMember(dest => dest.NumeroVenda, opt => opt.MapFrom(src => src.Sale.NumeroVenda))
                .ForMember(dest => dest.DataVenda, opt => opt.MapFrom(src => src.Sale.DataVenda))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Sale.ClienteId))
                .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Sale.ClienteNome))
                .ForMember(dest => dest.FilialId, opt => opt.MapFrom(src => src.Sale.FilialId))
                .ForMember(dest => dest.FilialNome, opt => opt.MapFrom(src => src.Sale.FilialNome))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.Sale.ValorTotal))
                .ForMember(dest => dest.Cancelada, opt => opt.MapFrom(src => src.Sale.Cancelada))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Sale.Itens));

            CreateMap<SaleItem, GetSaleItemResponse>();
        }
    }


}
