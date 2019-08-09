using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Services
{
    public interface IRecordsDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RecordsCollectionName { get; set; }
    }

    /// <summary>
    /// Allows simple access to configuration settings for the records data store.
    /// </summary>
    public class RecordsDatabaseSettings : IRecordsDatabaseSettings
    {
        public string RecordsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
