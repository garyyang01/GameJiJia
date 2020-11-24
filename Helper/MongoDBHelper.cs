using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameJiJia.Helper
{
    public static class MongoDBHelper
    {
        private static IMongoClient _client = new MongoClient("mongodb://127.0.0.1:27017/");

        public static async Task<List<BsonDocument>> read(string collectionName, BsonDocument filter = null, string dbName = "GJJMain")
        {
            var response = new List<BsonDocument>();
            if (filter == null)
            {
                filter = new BsonDocument();
            }
            IMongoDatabase database = _client.GetDatabase(dbName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        response.Add(document);
                    }
                }
            }

            return response;
        }

        public static async Task insert(object data, string collectionName, string dbName = "GJJMain")
        {
            IMongoDatabase database = _client.GetDatabase(dbName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            await collection.InsertOneAsync(data.ToBsonDocument());
        }
    }
}