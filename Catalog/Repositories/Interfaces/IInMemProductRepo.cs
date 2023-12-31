namespace WebAPI.Catalog.Repositories.Interfaces
{
    public interface IInMemProductRepo{
        Task<Product>? GetProductAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Guid id,Product product);
        Task DeleteProductAsync(Guid id);
    }
}