using System.ComponentModel.DataAnnotations;

namespace WebAPI.Catalog.DTOs
{
    public record CreateProductDTO
    {
        [Required]
        public string? Name { get; init; }
        [Required]
        [Range(0.01,int.MaxValue)]
        public decimal Price { get; init; }

    }
}