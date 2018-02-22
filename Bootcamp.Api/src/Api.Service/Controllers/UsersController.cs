using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Service.Dal;
using Api.Service.Helper;
using Api.Service.Models;
using Swashbuckle.Swagger.Annotations;
using Xlent.Lever.KeyTranslator.RestClients.Facade.Clients;
using Xlent.Lever.KeyTranslator.Sdk;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;

namespace Api.Service.Controllers
{
    [RoutePrefix("api/Users")]
    // TODO: enable [FulcrumAuthorize(AuthenticationRoleEnum.ExternalSystemUser)]
    public class UsersController : ApiController
    {
        private readonly ICustomerMasterClient _customerMasterClient;
        private readonly ITranslateClient _translateClient;

        public UsersController(ICustomerMasterClient customerMasterClient, ITranslateClient translateClient)
        {
            _customerMasterClient = customerMasterClient;
            _translateClient = translateClient;
        }

        //TODO: Tutorial 1 - Implement this method
        [Route("{id}")]
        [HttpGet]
        public async Task<User> GetAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            /* Code for translating the user type to approperiate context 
             * The call to translate maps a concept (the 'kind' of value to be translated) to
             * a clients context (each client is assigned a context which decides the range to translate from/to
             * See GetAllAsync // wiki for more insight in translation.
             */

            await Task.Yield(); //Remove this line
            throw new FulcrumNotImplementedException("Method GET /Users/{id} is not yet implemented - Part of tutorial 1");
        }

        /*
         * This method is already implemented for your convenience
         */
        [Route("")]
        [HttpGet]
        [SwaggerOperationFilter(typeof(SwaggerUserOperationFIlter))]
        public async Task<List<User>> GetAllAsync(string type = null)
        {
            if (!string.IsNullOrWhiteSpace(type))
            {
                await new BatchTranslate(_translateClient, "mobile-app", "customer-master")
                    .Add("user.type", type, translatedValue => type = translatedValue)
                    .ExecuteAsync();
            }

            var result = await _customerMasterClient.GetUsers(type);

            var translateUp = new BatchTranslate(_translateClient, "customer-master", "mobile-app");
            foreach (var user in result)
            {
                translateUp.Add("user.type", user.Type, translatedValue => user.Type = translatedValue);
            }
            await translateUp.ExecuteAsync();

            return result;
        }

        [Route("")]
        [HttpPost]
        public async Task<string> PostAsync(User user)
        {
            ServiceContract.RequireNotNull(user, nameof(user));
            ServiceContract.RequireValidated(user, nameof(user));

            await new BatchTranslate(_translateClient, "mobile-app", "customer-master")
                .Add("user.type", user.Type, translatedValue => user.Type = translatedValue)
                .ExecuteAsync();

            return await _customerMasterClient.AddUser(user);
        }

        //TODO: Tutorial 1 - Implement this method (optional)
        [Route("{id}")]
        [HttpPut]
        public async Task<User> PutAsync(string id, User user)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(user, nameof(user));
            ServiceContract.RequireValidated(user, nameof(user));

            await new BatchTranslate(_translateClient, "mobile-app", "customer-master")
                .Add("user.type", user.Type, translatedValue => user.Type = translatedValue)
                .ExecuteAsync();

            throw new FulcrumNotImplementedException("Method PUT /Users/{id} is not yet implemented - Part of tutorial 1");
        }

        //TODO: Tutorial 1 - Implement this method (optional)
        [Route("{id}")]
        [HttpDelete]
        public User DeleteOne(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            throw new FulcrumNotImplementedException("Method DELETE /Users/{ id } is not yet implemented - Part of tutorial 1");
        }

        /*
         * This method is already implemented for your convenience
         */
        [Route("")]
        [HttpDelete]
        public async Task DeleteAll()
        {
            await _customerMasterClient.DeleteUsers();
        }
    }
}
