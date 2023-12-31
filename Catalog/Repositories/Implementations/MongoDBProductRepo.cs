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
        public async Task CreateProductAsync(Product product)
        {
            await productsCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var filter=filterDefinitionBuilder.Eq(product=>product.Id, id);
            await productsCollection.DeleteOneAsync(filter);
        }

        public async Task<Product>? GetProductAsync(Guid id)
        {
            var filter=filterDefinitionBuilder.Eq(product=>product.Id, id);
            return await productsCollection.Find(filter).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await productsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProductAsync(Guid id, Product product)
        {
            var filter=filterDefinitionBuilder.Eq(i=>i.Id,id);
            await productsCollection.ReplaceOneAsync(filter, product);
        }
    }
}