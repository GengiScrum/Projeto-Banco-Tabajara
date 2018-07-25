using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ws_BancoTabajara.Api.Controllers.Common;

namespace Ws_BancoTabajara.Api.Controllers.Common
{
    [RoutePrefix("api/public")]
    public class PublicController : ApiControllerBase
    {
        [HttpGet]
        [Route("is-alive")]
        public IHttpActionResult IsAlive()
        {
            return Ok(true);
        }
    }
}
