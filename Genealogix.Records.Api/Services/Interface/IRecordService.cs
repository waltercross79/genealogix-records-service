using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Models;

namespace Genealogix.Records.Api.Services 
{
    /// <summary>
    /// Provides business logic and data layer access for records.
    /// </summary>
    public interface IRecordService
    {
        /// <summary>
        /// Searches the records store for all records that meet the search criteria.
        /// </summary>
        /// <param name="filters">Search criteria to match.</param>
        /// <returns>Collection of records that meet the criteria.</returns>
        IEnumerable<Record> Search(SearchFilters filters);

        /// <summary>
        /// Gets a record with matching ID from the store.
        /// Returns null, if no record found.
        /// </summary>
        /// <param name="id">Unique identifier of the record to locate.</param>
        /// <returns>Record with matching ID or null, if not found.</returns>
        Record GetById(string id);

        /// <summary>
        /// Tries to remove a record from the data store.
        /// </summary>
        /// <param name="record">Record to remove.</param>
        void Remove(Record record);

        /// <summary>
        /// Tries to remove a record from the data store using the record's unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the record to remove.</param>
        void Remove(string id);

        /// <summary>
        /// Adds new record to the data store.
        /// </summary>
        /// <param name="record">Record to add to the datastore.</param>
        /// <returns>Newly added record with updated unique identifier.</returns>
        Record Create(Record record);

        /// <summary>
        /// Replaces existing record with new one.
        /// </summary>
        /// <param name="id">Unique identifier of the record to replace.</param>
        /// <param name="record">Record to store in the data store instead of the old one.</param>
        void Update(string id, Record record);
    }
}