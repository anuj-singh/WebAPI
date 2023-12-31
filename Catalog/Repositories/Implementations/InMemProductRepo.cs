using WebAPI.Catalog.Repositories.Interfaces;

namespace WebAPI.Catalog.Repositories.Implemntations
{
    
    class InMemProductRepo : IInMemProductRepo
    {
        private readonly List<Product> products= new()
        {
            new Product{Id=Guid.NewGuid(),Name="Cricket Ball", Price=100, CreatedTime=DateTime.Now},
            new Product{Id=Guid.NewGuid(),Name="Cricket Bat", Price=500, CreatedTime=DateTime.Now},
            new Product{Id=Guid.NewGuid(),Name="Cricket Kit", Price=2000, CreatedTime=DateTime.Now},
        };
        public async Task CreateProductAsync(Product product)
        {
            products.Add(product);
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var index=products.FindIndex(p=>p.Id==id);
            products.RemoveAt(index);
            await Task.CompletedTask;
        }

        public async Task<Product>? GetProductAsync(Guid id)
        {
            var product= products.FirstOrDefault(p=>p.Id==id);
#pragma warning disable CS8603 // Possible null reference return.
            return await Task.FromResult(product);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(products);
        }

        public async Task UpdateProductAsync(Guid id,Product product)
        {
            var index= products.FindIndex(p=>p.Id==product.Id);
            products[index]=product;
            await Task.CompletedTask;
        }
    }
}