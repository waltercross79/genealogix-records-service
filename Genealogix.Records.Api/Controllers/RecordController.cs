using System;
using System.Collections.Generic;
using System.Linq;
using Genealogix.Records.Api.Models;
using Genealogix.Records.Api.Services;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            if(id <= 0)
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
            var result = _recordService.Search(filters);

            return result.ToList();
        }

        // POST api/record

        /// <summary>
        /// Creates new record and stored it in the data storage.
        /// </summary>
        /// <param name="record">Details of a new record to store.</param>
        /// <returns>Created record with unique identifier.</returns>
        [HttpPost]
        public ActionResult<Record> Create([FromBody] Record record) {
            throw new NotImplementedException();
        }

        // PUT api/record

        /// <summary>
        /// Updates an existing record in the data store.
        /// </summary>
        /// <param name="record">Existing record with updated properties.</param>
        /// <returns>Updated record after it was stored in the data store.</returns>
        [HttpPut]
        public ActionResult<Record> Update([FromBody] Record record) {
            throw new NotImplementedException();
        }

        // DELETE api/record/5

        /// <summary>
        /// Tries to remove a record from the system.
        /// </summary>
        /// <param name="id">Unique identifier of the record.</param>
        /// <returns>204 response code when successful.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a person to a record.
        /// </summary>
        /// <param name="id">Unique identifier of the record.</param>
        /// <param name="personInRecord">Details of the person to add to the record.</param>
        /// <returns>Service action result and optional message.</returns>
        [HttpPost("{id}/AddPerson")]
        public IActionResult AddPerson(int id, [FromBody] PersonInRecord personInRecord) 
        {
            throw new NotImplementedException();
        }
    }
}