using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Genealogix.Records.Api.Models 
{
    public sealed class PersonInRecord 
    {
        /// <summary>
        /// First name of the person listed in the record.
        /// </summary>
        /// <value></value>
        [BsonRequired]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person listed in the record.
        /// </summary>
        /// <value></value>
        [BsonRequired]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Internal identifier of the person in the known-persons' database.
        /// </summary>
        /// <value></value>
        public int ID { get; set; }

        /// <summary>
        /// Role of the person in the recorded event - mother|father|bride|groom...
        /// </summary>
        /// <value></value>
        [BsonRequired]
        [Required]
        public PersonRole Role { get; set; }

        private DateTime? _dob;
        /// <summary>
        /// Date of birth of the person in record, if known.
        /// </summary>
        /// <value></value>
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Unspecified)]
        public DateTime? DOB 
        { 
            get
            {
                return _dob;
            }
            set
            {
                if(value == null)
                {
                    _dob = null;
                }
                else 
                {
                    _dob = value.Value.Date;
                }
            }
        }       
    }
}