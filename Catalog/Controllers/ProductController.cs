using Microsoft.AspNetCore.Mvc;
using WebAPI.Catalog;
using WebAPI.Catalog.DTOs;
using WebAPI.Catalog.Extensions;
using WebAPI.Catalog.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController:ControllerBase{
        private readonly IInMemProductRepo _productRepo;
        public ProductController(IInMemProductRepo inMemProductRepo)
        {
            _productRepo=inMemProductRepo;
        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(){
            var products=(await _productRepo.GetProductsAsync())
                            .Select(p=>p.ToProductDTO());
            if (products is not null)
            {
                return Ok(products);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var product=  await _productRepo.GetProductAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (product is not null)
            {
                return Ok(product.ToProductDTO());
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO createProductDTO){
            Product product=new(){
                Id=Guid.NewGuid(),
                Name=createProductDTO.Name,
                Price=createProductDTO.Price,
                CreatedTime=DateTime.Now,
            };
            await  _productRepo.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct),new {id=product.Id},product.ToProductDTO());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id,UpdateProductDTO updateProductDTO){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var product= await _productRepo.GetProductAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (product is null)
            {
                return NotFound();
            }
            Product updatedProduct= product with{
                Name=updateProductDTO.Name,
                Price=updateProductDTO.Price,
            };
            await _productRepo.UpdateProductAsync(id,updatedProduct);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id){
           await  _productRepo.DeleteProductAsync(id);
            return NoContent();
        }
    }
}