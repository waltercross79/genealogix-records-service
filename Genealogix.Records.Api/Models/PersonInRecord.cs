using System;

namespace Genealogix.Records.Api.Models 
{
    public sealed class PersonInRecord 
    {
        /// <summary>
        /// First name of the person listed in the record.
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person listed in the record.
        /// </summary>
        /// <value></value>
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
        public PersonRole Role { get; set; }

        /// <summary>
        /// Date of birth of the person in record, if known.
        /// </summary>
        /// <value></value>
        public DateTime? DOB { get; set; }
    }
}