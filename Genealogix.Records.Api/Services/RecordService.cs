using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Models;
using MongoDB.Driver;

namespace Genealogix.Records.Api.Services 
{
    /// <summary>
    /// Provides access to records data store.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly MongoClientBase _db;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="mongoClient">MongoDB client implementation.</param>
        public RecordService(MongoClientBase mongoClient)
        {
            _db = mongoClient;
        }

        public Record GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Record> Search(SearchFilters filters)
        {
            throw new System.NotImplementedException();
        }
    }
}