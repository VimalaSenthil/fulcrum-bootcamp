using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Xlent.Lever.Authentication.Sdk.Attributes;
using Xlent.Lever.BusinessEvents.Sdk;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.MultiTenant.Model;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.Platform.Authentication;

namespace Api.Service.Controllers
{
    [RoutePrefix("api/BusinessEvents")]
    [FulcrumAuthorize(AuthenticationRoleEnum.ExternalSystemUser)]
    public class BusinessEventsController : ApiController
    {
        private readonly ITokenRefresherWithServiceClient _tokenRefresher;
        private readonly Tenant _tenant;


        private static readonly string BusinessEventsBaseUrl = ConfigurationManager.AppSettings["BusinessEvents.Url"];

        public BusinessEventsController(Tenant tenant, ITokenRefresherWithServiceClient tokenRefresher)
        {
            _tenant = tenant;
            _tokenRefresher = tokenRefresher;
        }

        [Route("Publish/{entityName}/{eventName}/{majorVersion}/{minorVersion}")]
        [HttpPost]
        public async Task PublishAsync(string entityName, string eventName, int majorVersion, int minorVersion, string clientName, JObject content)
        {
            ServiceContract.RequireNotNullOrWhitespace(entityName, nameof(entityName));
            ServiceContract.RequireNotNullOrWhitespace(eventName, nameof(eventName));
            ServiceContract.RequireGreaterThanOrEqualTo(1, majorVersion, nameof(majorVersion));
            ServiceContract.RequireGreaterThanOrEqualTo(0, minorVersion, nameof(minorVersion));
            ServiceContract.RequireNotNullOrWhitespace(clientName, nameof(clientName));

            await new BusinessEvents(BusinessEventsBaseUrl, _tenant, _tokenRefresher.GetServiceClient())
                .PublishAsync(entityName, eventName, majorVersion, minorVersion, clientName, content);
        }
    }
}
