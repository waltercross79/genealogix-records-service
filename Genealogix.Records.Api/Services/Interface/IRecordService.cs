using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Models;

namespace Genealogix.Records.Api.Services 
{
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
        Record GetById(int id);
    }
}