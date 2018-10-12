using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xlent.Lever.Authentication.Sdk.Attributes;
using Xlent.Lever.Libraries2.Core.Application;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Logging;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Api.Service.Controllers
{
    [RoutePrefix("api/Logs")]
    [FulcrumAuthorize(AuthenticationRoleEnum.ExternalSystemUser)]
    public class LogController : ApiController
    {


        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> LogAsync(LogBatch batch)
        {
            ServiceContract.RequireNotNull(batch, nameof(batch));
            ServiceContract.RequireValidated(batch, nameof(batch));

            await FulcrumApplication.Setup.FullLogger.LogAsync(batch);
            return new StatusCodeResult(HttpStatusCode.Accepted, this);
        }
    }
}
