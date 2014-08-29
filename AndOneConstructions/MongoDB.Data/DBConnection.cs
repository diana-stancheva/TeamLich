namespace MongoDB.Data
{
    using MongoDB.Driver;
    using System;
    using System.Linq;

    public static class DBConnection
    {
        public static MongoDatabase GetDBConnection(string databaseName)
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(databaseName);

            return database;
        }
    }
}
