﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;
using Ws_BancoTabajara.Api.Filters;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapApiRoutes();
            config.ConfigureJsonSerialization();
            config.ConfigureXMLSerialization();
            config.Filters.Add(new ExceptionHandlerAttribute());
        }

        private static void MapApiRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Ws-BancoTabajara.Api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            );
        }

        private static void ConfigureJsonSerialization(this HttpConfiguration config)
        {
            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.Formatting = Formatting.None;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("aplicarion/xml"));
        }

        private static void ConfigureXMLSerialization(this HttpConfiguration config)
        {
            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            var dcs = new DataContractSerializer(typeof(BankAccount), null, int.MaxValue, false, true, null);
            xml.SetSerializer<BankAccount>(dcs);
        }
    }
}
