using System;
using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Db;
using Genealogix.Records.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Genealogix.Records.Api.Services 
{
    /// <summary>
    /// Provides access to records data store.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly IMongoCollection<Record> _records;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="mongoClient">MongoDB client implementation.</param>
        public RecordService(IRecordsDatabaseClientFactory clientFactory, IRecordsDatabaseSettings settings)
        {
            var client = clientFactory.GetClient();
            var database = client.GetDatabase(settings.DatabaseName, null);

            _records = database.GetCollection<Record>(settings.RecordsCollectionName, null);
        }

        public Record GetById(string id)
        {
            return _records.Find<Record>(r => r.ID == id).FirstOrDefault();
        }

        public IEnumerable<Record> Search(SearchFilters filters)
        {
            var builder = Builders<Record>.Filter;

            List<RecordType> recordTypes = new List<RecordType>();
            if (filters.IncludeBirths)
                recordTypes.Add(RecordType.Birth);
            if (filters.IncludeDeaths)
                recordTypes.Add(RecordType.Death);
            if (filters.IncludeMarriages)
                recordTypes.Add(RecordType.Marriage);

            var filter = builder.In(r => r.RecordType, recordTypes);

            if (filters.RecordDateFrom.HasValue)
                filter &= builder.Gte(r => r.RecordDate, filters.RecordDateFrom.Value);
            if (filters.RecordDateTo.HasValue)
                filter &= builder.Lte(r => r.RecordDate, filters.RecordDateTo.Value);

            if (!String.IsNullOrWhiteSpace(filters.Street))
            {
                filter &= builder.Eq(r => r.Street, filters.Street);

            }
            if (!String.IsNullOrWhiteSpace(filters.Number))
            {
                filter &= builder.Eq(r => r.Number, filters.Number);
            }

            if (!String.IsNullOrWhiteSpace(filters.Town))
            {
                filter &= builder.Eq(r => r.Town, filters.Town);
            }

            if (!String.IsNullOrWhiteSpace(filters.Country))
            {
                filter &= builder.Eq(r => r.Country, filters.Country);
            }

            if (!String.IsNullOrWhiteSpace(filters.Folio))
            {
                filter &= builder.Eq(r => r.Folio, filters.Folio);
            }

            if (!String.IsNullOrWhiteSpace(filters.Registry))
            {
                filter &= builder.Eq(r => r.Registry, filters.Registry);
            }
            
            if (!String.IsNullOrWhiteSpace(filters.FirstName))
            {
                filter &= builder.Eq("Persons.FirstName", filters.FirstName);
            }

            if (!String.IsNullOrWhiteSpace(filters.LastName))
            {
                filter &= builder.Eq("Persons.LastName", filters.LastName);
            }

            return _records.Find<Record>(filter).ToList();
        }

        public Record Create(Record record)
        {
            _records.InsertOne(record);
            return record;
        }

        public void Update(string id, Record record)
        {
            _records.ReplaceOne<Record>(r => r.ID == id, record);
        }

        public void Remove(string id)
        {
            _records.DeleteOne<Record>(r => r.ID == id);
        }

        public void Remove(Record record)
        {
            _records.DeleteOne<Record>(r => r.ID == record.ID);
        }
    }
}