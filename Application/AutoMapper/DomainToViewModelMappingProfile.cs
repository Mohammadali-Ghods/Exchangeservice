﻿using Application.ViewModel;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<CustomerExchange, CustomerExchangeViewModel>();
            CreateMap<Symbol, SymbolViewModel>();
        }
    }
}
