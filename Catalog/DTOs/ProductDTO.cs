namespace WebAPI.Catalog.DTOs
{
    public record ProductDTO
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public DateTime CreatedTime { get; init; }
    }
}