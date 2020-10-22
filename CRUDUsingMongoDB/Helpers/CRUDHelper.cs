using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDUsingMongoDB.Helpers
{
    public static class CRUDHelper
    {
        private static IMongoClient client;
        private static IMongoDatabase database;
        public static bool InsertRecord<T>(string databasename, string table, List<T> entity)
        {
            client = new MongoClient();
            database = client.GetDatabase(databasename);
            var collection = database.GetCollection<T>(table);
            var result = collection.InsertManyAsync(entity);
            if (result.IsCompletedSuccessfully)
            {
                return true;
            }

            return false;

        }

        public static async Task<List<T>> ListRecordsAsync<T>(string databasename, string table)
        {
            client = new MongoClient();
            database = client.GetDatabase(databasename);
            var collection = database.GetCollection<T>(table);
            var result = await collection.Find(new BsonDocument()).ToListAsync();
            return result;
        }

        public static async Task<T> LoadRecordById<T>(string databasename, string table, Guid guid)
        {
            client = new MongoClient();
            database = client.GetDatabase(databasename);
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", guid);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public static async Task<bool> DeleteRecordById<T>(string databasename, string table, Guid guid)
        {
            client = new MongoClient();
            database = client.GetDatabase(databasename);
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", guid);
            var result = await collection.DeleteOneAsync(filter);

            if (result.IsAcknowledged)
            {
                return true;
            }

            return false;

        }

        public static async Task<bool> UpdateRecordById<T>(string databasename, string table, Guid guid, T entity)
        {
            client = new MongoClient();
            database = client.GetDatabase(databasename);
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", guid);
            try
            {
                var result = await collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });
                if (result.IsAcknowledged)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

                throw;
            }
            return false;

        }

    }
}
