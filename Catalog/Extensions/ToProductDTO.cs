using WebAPI.Catalog.DTOs;

namespace WebAPI.Catalog.Extensions
{
    public static class Extensions
    {
        public static ProductDTO ToProductDTO(this Product product){
            return new ProductDTO{
                Name=product.Name,
                Id=product.Id,
                Price=product.Price,
                CreatedTime=product.CreatedTime
            };
        }
    }
}