using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAPI.Catalog.Repositories.Interfaces;

namespace WebAPI.Catalog.Repositories.Implemntations
{
    class MongoDBProductRepo : IInMemProductRepo
    {
        private const string DatabaseName="Catalog";
        private const string collectionName="products";
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterDefinitionBuilder=Builders<Product>.Filter;
        
        public MongoDBProductRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database=mongoClient.GetDatabase(DatabaseName);
            productsCollection=database.GetCollection<Product>(collectionName);
            
        }
        public void CreateProduct(Product product)
        {
            productsCollection.InsertOne(product);
        }

        public void DeleteProduct(Guid id)
        {
            var filter=filterDefinitionBuilder.Eq(product=>product.Id, id);
            productsCollection.DeleteOne(filter);
        }

        public Product? GetProduct(Guid id)
        {
            var filter=filterDefinitionBuilder.Eq(product=>product.Id, id);
            return productsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return productsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateProduct(Guid id, Product product)
        {
            var filter=filterDefinitionBuilder.Eq(i=>i.Id,id);
            productsCollection.ReplaceOne(filter, product);
        }
    }
}