namespace MongoDB.Data.Context
{
    using MongoDB.Bson;
    using MongoDB.Data;
    using MongoDB.Driver;
    using System;

    class MongoDBProject
    {
        public ObjectId Id { get; set; }

        public string ProjectName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Client { get; set; }

        public string BuildingType { get; set; }

        public string BuildingAddress { get; set; }

        public string BuildingTown { get; set; }

        public string ConstructionSiteName { get; set; }

        public void PrintAllEntities(string dbName = "AndOneConstructions", string collectionName = "Employees")
        {
            var database = DBConnection.GetDBConnection(dbName);

            var collection = database.GetCollection<MongoDBProject>(collectionName);

            foreach (MongoDBProject item in collection.FindAll())
            {
                Console.WriteLine(item.ToJson());
            }
        }

        public MongoCollection<MongoDBProject> GetAllEntities(string dbName, string collectionName)
        {
            var database = DBConnection.GetDBConnection(dbName);

            var collection = database.GetCollection<MongoDBProject>(collectionName);

            return collection;
        }
    }
}
