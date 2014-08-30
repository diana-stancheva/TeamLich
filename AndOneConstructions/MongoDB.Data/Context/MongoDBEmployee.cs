namespace MongoDB.Data.Context
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Data;
    using MongoDB.Driver;

    public class MongoDBEmployee : IMongoEntity<MongoDBEmployee>
    {
        public ObjectId Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public void PrintAllEntities(string dbName = "appharbor_f580ae0d-6ef8-4aac-b142-db0920bfddac", string collectionName = "Employees")
        {
            var database = DBConnection.GetDBConnection(dbName);

            var collection = database.GetCollection<MongoDBEmployee>(collectionName);

            foreach (MongoDBEmployee item in collection.FindAll())
            {
                Console.WriteLine(item.ToJson());
            }
        }

        public MongoCollection<MongoDBEmployee> GetAllEntitiesAsCollection(string dbName = "appharbor_f580ae0d-6ef8-4aac-b142-db0920bfddac", string collectionName = "Employees")
        {
            var database = DBConnection.GetDBConnection(dbName);

            var collection = database.GetCollection<MongoDBEmployee>(collectionName);

            return collection;
        }
    }
}