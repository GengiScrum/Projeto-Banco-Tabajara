using Microsoft.Owin;
using Owin;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Ws_BancoTabajara.Api.Startup))]
namespace Ws_BancoTabajara.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseWebApi(config);
        }
    }
}