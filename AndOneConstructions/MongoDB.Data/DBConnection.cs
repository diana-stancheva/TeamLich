namespace MongoDB.Data
{
    using System;
    using System.Linq;

    using MongoDB.Driver;

    public static class DBConnection
    {
        public static MongoDatabase GetDBConnection(string databaseName)
        {
            //string connectionString = "mongodb://localhost";
            //mongodb://<dbuser>:<dbpassword>@ds063929.mongolab.com:63929
            string connectionString = "mongodb://teamlich:teamlich@ds063929.mongolab.com:63929/appharbor_f580ae0d-6ef8-4aac-b142-db0920bfddac";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(databaseName);

            return database;
        }
    }
}
