using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Mapping;
using Ws_BancoTabajara.Controller.Tests.Common;

namespace Ws_BancoTabajara.Controller.Tests.Initializer
{
    public class TestControllerBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            AutoMapperTestsInitializer.Reset();
            AutoMapperTestsInitializer.Initialize();
        }
    }
}
