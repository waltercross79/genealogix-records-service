using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Db
{
    public interface IRecordsDatabaseClientFactory
    {
        MongoClientBase GetClient();
    }
}
