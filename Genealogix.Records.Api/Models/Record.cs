using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genealogix.Records.Api.Models 
{
    public sealed class Record 
    {
        public Record()
        {
            this.Persons = new List<PersonInRecord>();
            this.ImageIdentifiers = new List<string>();
        }

        /// <summary>
        /// Unique identifier of the record.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        /// <summary>
        /// Date when the record was made in the registry.
        /// </summary>
        [BsonRequired]
        [Required]
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// Type of record - birth|death|marriage.
        /// </summary>
        [BsonRequired]
        [Required]
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
        public IList<PersonInRecord> Persons { get; set; }

        /// <summary>
        /// Collection of unique identifiers of all images associated with the record.
        /// </summary>
        public IEnumerable<string> ImageIdentifiers { get; set; }

        internal void AddPerson(PersonInRecord pir)
        {
            // Validation logic should come here.
            // Throw exception when invalid attempt - like adding two mothers to a record.

            this.Persons.Add(pir);
        }
    }
}