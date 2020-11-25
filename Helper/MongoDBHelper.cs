using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameJiJia.Helper
{
    public class MongoDBHelper
    {
        private static IMongoClient _client = new MongoClient("mongodb://127.0.0.1:27017/");

        public static async Task<List<T>> Read<T>(string collectionName, BsonDocument filter = null, string dbName = "GJJMain")
        {
            var response = new List<T>();
            if (filter == null)
            {
                filter = new BsonDocument();
            }

            try
            {
                IMongoDatabase database = _client.GetDatabase(dbName);
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                using (var cursor = await collection.FindAsync(filter))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (BsonDocument document in batch)
                        {
                            response.Add(BsonSerializer.Deserialize<T>(document));
                        }
                    }
                }

                return response;
            }
            catch(BsonException e)
            {
                throw e;
                return null;
            }
        }

        public static async Task<bool> Insert(object data, string collectionName, string dbName = "GJJMain")
        {
            try
            {
                IMongoDatabase database = _client.GetDatabase(dbName);
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                await collection.InsertOneAsync(data.ToBsonDocument());
                return true;
            }
            catch(BsonException e)
            {
                throw;
                return false;
            }
        }
    }
}