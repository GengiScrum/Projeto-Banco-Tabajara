using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Controller.Tests.Common
{
    public static class AutoMapperTestsInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperTestsInitializer));
            });
        }

        public static void Reset() => Mapper.Reset();
    }
}
