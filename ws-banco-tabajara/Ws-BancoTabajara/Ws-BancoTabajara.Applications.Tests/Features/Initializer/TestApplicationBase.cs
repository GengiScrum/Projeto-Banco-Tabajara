using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Mapping;

namespace Ws_BancoTabajara.Applications.Tests.Features.Initializer
{
    public class TestApplicationBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            AutoMapperInitializer.Reset();
            AutoMapperInitializer.Initialize();
        }
    }
}
