﻿using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<ListSalesRequest, ListSalesCommand>();
            CreateMap<Sale, ListSalesResponse>();
        }
    }

}
