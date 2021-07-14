using AutoMapper;
using GloboTicket.TicketManagement.Application.Features.Events;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Application.Features.Profiles
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
        }
    }
}
