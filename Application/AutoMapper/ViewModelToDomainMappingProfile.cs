using Application.ViewModel;
using AutoMapper;
using Domain.Commands;
using Domain.Commands.CustomerExchangeCommands;

namespace Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CallUpViewModel, CallUpCommand>();
            CreateMap<FinalizeTransactionViewModel, FinalizeTransactionCommand>();
        }
    }
}
