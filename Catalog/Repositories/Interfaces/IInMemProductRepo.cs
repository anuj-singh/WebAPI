namespace WebAPI.Catalog.Repositories.Interfaces
{
    public interface IInMemProductRepo{
        Product? GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Guid id,Product product);
        void DeleteProduct(Guid id);
    }
}