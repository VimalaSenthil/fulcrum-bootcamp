using System.Threading.Tasks;
using System.Web.Http;
using Api.Service.Dal;
using Xlent.Lever.Authentication.Sdk.Attributes;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Api.Service.Controllers
{
    [RoutePrefix("api/VisualNotifications")]
    [FulcrumAuthorize(AuthenticationRoleEnum.ExternalSystemUser)]
    public class VisualNotificationsController : ApiController
    {
        private readonly IVisualNotificationClient _client;
        public VisualNotificationsController(IVisualNotificationClient client)
        {
            _client = client;
        }

        [HttpPost]
        [Route("Success")]
        public async Task SuccessAsync()
        {
            await _client.VisualNotificationSuccessAsync();
        }

        [HttpPost]
        [Route("Warning")]
        public async Task WarningAsync()
        {
            await _client.VisualNotificationWarningAsync();
        }

        [HttpPost]
        [Route("Error")]
        public async Task ErrorAsync()
        {
            await _client.VisualNotificationErrorAsync();
        }
    }
}
