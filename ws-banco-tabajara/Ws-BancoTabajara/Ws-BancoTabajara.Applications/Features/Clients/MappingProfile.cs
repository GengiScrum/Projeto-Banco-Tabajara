using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Applications.Features.Clients
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientRegisterCommand, Client>();
            CreateMap<Client, ClientViewModel>()
                .ForMember(c => c.BirthDate, mo => mo.MapFrom(cq => cq.BirthDate));
            CreateMap<ClientUpdateCommand, Client>();
            CreateMap<List<Client>, List<ClientViewModel>>();
        }
    }
}
