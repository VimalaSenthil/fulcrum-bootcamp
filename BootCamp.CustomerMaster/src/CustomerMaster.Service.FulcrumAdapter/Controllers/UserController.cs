using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerMaster.Service.FulcrumAdapter.Contract;
using Xlent.Lever.Authentication.Sdk.Attributes;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.Crud.Interfaces;

namespace CustomerMaster.Service.FulcrumAdapter.Controllers
{
    /// <inheritdoc />
    [FulcrumAuthorize(AuthenticationRoleEnum.InternalSystemUser)]
    [RoutePrefix("api/Users")]
    public class UserController : ApiController
    {
        private readonly ICrud<User, string> _persistence;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="persistence">How we deal with persistence</param>
        public UserController(ICrud<User, string> persistence)
        {
            _persistence = persistence;
        }

        /// <summary>
        /// Create a new <paramref name="user"/> record.
        /// </summary>
        /// <returns>The new id for the record.</returns>
        [HttpPost]
        [Route("")]
        public async Task<string> Create([FromBody] User user)
        {
            ServiceContract.RequireNotNull(user, nameof(user));
            ServiceContract.RequireValidated(user, nameof(user));

            return await _persistence.CreateAsync(user);
        }

        /// <summary>
        /// Read the user record with id <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public async Task<User> Read(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            return await _persistence.ReadAsync(id);
        }

        /// <summary>
        /// Read all user records with the specified <paramref name="type"/>. Null for type means all records.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<User>> ReadAll(string type = null)
        {
            var users = await _persistence.ReadAllAsync();
            //var users = (IEnumerable<User>)new PageEnvelopeEnumerable<User>((offset, t) => _persistance.ReadAllAsync(offset).Result);
            if (!string.IsNullOrWhiteSpace(type))
            {
                users = users.Where(x => x.Type == type);
            }
            return users;
        }

        /// <summary>
        /// Updated the user record with id <paramref name="id"/>.
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task Update(string id, User user)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireValidated(user, nameof(user));

            await _persistence.UpdateAsync(user.Id, user);
        }

        /// <summary>
        /// Delete the user record with id <paramref name="id"/>.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            await _persistence.DeleteAsync(id);
        }

        /// <summary>
        /// Delete all user records.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task DeleteAll()
        {
            await _persistence.DeleteAllAsync();
        }
    }
}
