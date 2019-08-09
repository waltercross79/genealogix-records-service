using System;

namespace Genealogix.Records.Api.Controllers
{
    public sealed class SearchFilters 
    {
        /// <summary>
        /// The earliest date on or after which records will be included.
        /// Ignore if null.
        /// </summary>
        public DateTime? RecordDateFrom { get; set; }

        /// <summary>
        /// The latest data on or before which records will be included.
        /// Ignore if null.
        /// </summary>
        public DateTime? RecordDateTo { get; set; }

        /// <summary>
        /// Include birth records, if <c>true</c>. Exclude otherwise.
        /// </summary>
        public bool IncludeBirths { get; set; }

        /// <summary>
        /// Include death records, if <c>true</c>. Exclude otherwise.
        /// </summary>
        public bool IncludeDeaths { get; set; }

        /// <summary>
        /// Include wedding records, if <c>true</c>. Exclude otherwise.
        /// </summary>
        public bool IncludeMarriages { get; set; }

        /// <summary>
        /// Only include records with matching street name.
        /// Ignore if empty or null.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Only include records with matching house number.
        /// Ignore if empty or null.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Only include records with matching town.
        /// Ignore if empty or null.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Only include records with matching country.
        /// Ignore if empty or null.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Only include records with matching folio.
        /// Ignore if empty or null.
        /// </summary>
        public string Folio { get; set; }

        /// <summary>
        /// Only include records with matching registry.
        /// Ignore if empty or null.
        /// </summary>
        public string Registry { get; set; }

        /// <summary>
        /// Only include records connected to people with matching first name.
        /// Ignore if empty or null.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Only include records connected to people with matching last name.
        /// Ignore if empty or null.
        /// </summary>
        public string LastName { get; set; }
    }
}