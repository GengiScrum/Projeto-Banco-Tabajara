using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Mapping;

namespace Ws_BancoTabajara.Controller.Tests.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApiControllerBaseDummy, ApiControllerBaseDummy>();
        }
    }
}
