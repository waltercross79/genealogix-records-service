using System.Collections.Generic;
using Genealogix.Records.Api.Controllers;
using Genealogix.Records.Api.Models;
using Genealogix.Records.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Genealogix.Records.Api.Db;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Tests
{
    [TestClass]
    public class RecordServiceTests
    {
        private IRecordService _service;

        private Mock<IRecordsDatabaseClientFactory> _dbClientFactory;
        private Mock<IMongoDatabase> _db;
        private Mock<IMongoCollection<Record>> _dbRecordCollection;
        private IRecordsDatabaseSettings _dbSettings;
        private Mock<MongoClientBase> _dbClient;

        private const string CONNECTION_STRING = "CONNECTION_STRING";
        private const string RECORD_COLLECTION_NAME = "RECORDS_COLLECTION";
        private const string DATABASE_NAME = "RECORDS_DATABASE";
        private const string RECORD_ID = "ABCD";

        [TestInitialize]
        public void Init() {

            SetUpData();
            SetUpMocks();

            _service = new RecordService(_dbClientFactory.Object, _dbSettings);
        }

        private void SetUpData() 
        {
            _dbSettings = new RecordsDatabaseSettings {
                ConnectionString = CONNECTION_STRING,
                RecordsCollectionName = RECORD_COLLECTION_NAME,
                DatabaseName = DATABASE_NAME
            };
        }

        private void SetUpMocks() 
        {            
            _dbRecordCollection = new Mock<IMongoCollection<Record>>();

            _db = new Mock<IMongoDatabase>();
            _db.Setup(x => x.GetCollection<Record>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(_dbRecordCollection.Object);

            _dbClient = new Mock<MongoClientBase>();
            _dbClient.Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(_db.Object);

            _dbClientFactory = new Mock<IRecordsDatabaseClientFactory>();
            _dbClientFactory.Setup(x => x.GetClient())
                .Returns(_dbClient.Object);
        }

        [TestMethod]
        public void test_ctor_GetsRecordCollectionFromDatabase()
        {
            _dbClient.Verify(x => x.GetDatabase(DATABASE_NAME, null));
            _db.Verify(x => x.GetCollection<Record>(RECORD_COLLECTION_NAME, null));
            _dbClientFactory.Verify(x => x.GetClient());
        }
    }
}
