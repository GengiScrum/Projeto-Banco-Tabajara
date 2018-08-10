﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Ws_BancoTabajara.Api.IoC;
using Ws_BancoTabajara.Applications.Mapping;

namespace Ws_BancoTabajara.Api
{
    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SimpleInjectorContainer.Initializer();
            AutoMapperInitializer.Initialize();
        }
    }
}
