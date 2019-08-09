using System;
using System.Collections.Generic;
using System.Linq;
using Genealogix.Records.Api.Models;
using Genealogix.Records.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Genealogix.Records.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        // GET api/record/5

        /// <summary>
        /// Tries to find and return a record by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the record to get.</param>
        /// <returns>Single record with matching unique identifier.</returns>
        [HttpGet("{id:length(24)}")]
        public ActionResult<Record> Get(string id)
        {
            if(String.IsNullOrWhiteSpace(id))
                return NotFound();

            Record result = _recordService.GetById(id);

            if(result == null)
                return NotFound();

            return result;
        }

        // GET api/record?filter1=&filter2=

        /// <summary>
        /// Gets list of records that meet the filter criteria.
        /// </summary>
        /// <param name="filters">Filters that must be met by the records to be included in the result.</param>
        /// <returns>Collection of records that meet the filter criteria.</returns>
        [HttpGet]
        public ActionResult<List<Record>> Get([FromQuery] SearchFilters filters) 
        {
            List<Record> result = new List<Record>();

            if(filters.IncludeBirths || filters.IncludeDeaths || filters.IncludeMarriages)
                result = _recordService.Search(filters).ToList();

            return result;
        }

        // POST api/record

        /// <summary>
        /// Creates new record and stores it in the data storage.
        /// </summary>
        /// <param name="record">Details of a new record to store.</param>
        /// <returns>Created record with unique identifier.</returns>
        [HttpPost]
        public ActionResult<Record> Create([FromBody] Record record) {
            var result = _recordService.Create(record);

            return result;
        }

        // PUT api/record

        /// <summary>
        /// Updates an existing record in the data store.
        /// </summary>
        /// <param name="record">Existing record with updated properties.</param>
        /// <returns>Updated record after it was stored in the data store.</returns>
        [HttpPut("{id:length(24)}")]
        public ActionResult<Record> Update(string id, [FromBody] Record record) {

            var r = _recordService.GetById(id);

            if (r == null)
                return NotFound();

            _recordService.Update(id, record);

            return record;
        }

        // DELETE api/record/5

        /// <summary>
        /// Tries to remove a record from the system.
        /// </summary>
        /// <param name="id">Unique identifier of the record.</param>
        /// <returns>204 response code when successful.</returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var r = _recordService.GetById(id);

            if (r == null)
                return NotFound();

            _recordService.Remove(r.ID);

            return NoContent();
        }

        /// <summary>
        /// Adds a person to a record.
        /// </summary>
        /// <param name="id">Unique identifier of the record.</param>
        /// <param name="personInRecord">Details of the person to add to the record.</param>
        /// <returns>Service action result and optional message.</returns>
        [HttpPost("{id:length(24)}/AddPerson")]
        public IActionResult AddPerson(string id, [FromBody] PersonInRecord personInRecord) 
        {
            var r = _recordService.GetById(id);

            if (r == null)
                return NotFound();

            r.AddPerson(personInRecord);

            _recordService.Update(r.ID, r);

            return NoContent();
        }
    }
}