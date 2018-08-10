using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BankAccount, BankAccountViewModel>()
                .ForMember(b => b.ClientName, m => m.MapFrom(v => v.Client.Name));
            CreateMap<BankAccountUpdateCommand, BankAccount>()
                .ForPath(b => b.Client.Id, m => m.MapFrom(v => v.ClientId));
            CreateMap<BankAccountRegisterCommand, BankAccount>();
        }
    }
}
