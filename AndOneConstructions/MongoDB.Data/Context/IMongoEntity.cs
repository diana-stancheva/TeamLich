namespace MongoDB.Data.Context
{
    using MongoDB.Driver;

    interface IMongoEntity<T>
    {
        void PrintAllEntities(string dbName, string collctionName);

        MongoCollection<T> GetAllEntitiesAsCollection(string dbName, string collctionName);
    }
}
