using System;
using System.Collections.Generic;

namespace Genealogix.Records.Api.Models 
{
    public sealed class Record 
    {
        public Record()
        {
            this.Persons = new List<PersonInRecord>();
            this.ImageIdentifiers = new List<string>();
        }

        public int ID { get; set; }

        /// <summary>
        /// Date when the record was made in the registry.
        /// </summary>
        public DateTime RecordDate { get; set; } 

        /// <summary>
        /// Type of record - birth|death|marriage.
        /// </summary>
        public RecordType RecordType { get; set; }

        /// <summary>
        /// Optional street name from the record book.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Optional street number from the record.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Optional town/city/village name where the recorded event took place.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Optional country where the event took place.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Optional identifier of the folio in the registry book.
        /// </summary>
        public string Folio { get; set; }

        /// <summary>
        /// Optional registry book name/title.
        /// </summary>
        public string Registry { get; set; }

        /// <summary>
        /// Collection of persons identified in the record.
        /// </summary>
        public IEnumerable<PersonInRecord> Persons { get; set; }

        /// <summary>
        /// Collection of unique identifiers of all images associated with the record.
        /// </summary>
        public IEnumerable<string> ImageIdentifiers { get; set; }
    }
}