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
        public void CreateProduct(Product product)
        {
            products.Add(product);
        }

        public void DeleteProduct(Guid id)
        {
            var index=products.FindIndex(p=>p.Id==id);
            products.RemoveAt(index);
        }

        public Product? GetProduct(Guid id)
        {
            return products.FirstOrDefault(p=>p.Id==id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public void UpdateProduct(Guid id,Product product)
        {
            var index= products.FindIndex(p=>p.Id==product.Id);
            products[index]=product;
        }
    }
}