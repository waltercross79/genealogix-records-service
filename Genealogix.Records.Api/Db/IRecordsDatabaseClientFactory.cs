using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Db
{
    /// <summary>
    /// Defines methods that need to be implemented and provide access to an instance of 
    /// MongoDB client.
    /// </summary>
    public interface IRecordsDatabaseClientFactory
    {
        /// <summary>
        /// Gets an instance of the MongoDB client object.
        /// </summary>
        /// <returns>MongoDB client.</returns>
        MongoClientBase GetClient();
    }
}
