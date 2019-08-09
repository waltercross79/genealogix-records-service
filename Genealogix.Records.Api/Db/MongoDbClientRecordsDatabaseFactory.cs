using Genealogix.Records.Api.Services;
using MongoDB.Driver;

namespace Genealogix.Records.Api.Db
{
    /// <summary>
    /// Helper class that needs to be registered with DI container as a singleton.
    /// Provides MongoDB client instance.
    /// </summary>
    internal sealed class MongoDbClientRecordsDatabaseFactory : IRecordsDatabaseClientFactory
    {
        private MongoClient _client;
        private object _lock = new object();

        private IRecordsDatabaseSettings _settings;

        public MongoDbClientRecordsDatabaseFactory(IRecordsDatabaseSettings settings)
        {
            _settings = settings;
        }

        public MongoClientBase GetClient()
        {
            if(_client == null)
            {
                lock(_lock) {
                    _client = new MongoClient(_settings.ConnectionString);
                }
            }

            return _client;
        }
    }
}

