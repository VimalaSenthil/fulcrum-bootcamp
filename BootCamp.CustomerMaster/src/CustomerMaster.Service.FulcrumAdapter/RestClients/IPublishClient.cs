﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CustomerMaster.Service.FulcrumAdapter.RestClients
{
    /// <summary>
    /// Publish business events
    /// </summary>
    public interface IPublishClient
    {
        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="id">The publication id</param>
        /// <param name="eventBody">The event body</param>
        Task PublishAsync(Guid id, JObject eventBody);
    }
}