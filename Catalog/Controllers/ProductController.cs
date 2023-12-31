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
        private readonly IInMemProductRepo _inMemProductRepo;
        public ProductController(IInMemProductRepo inMemProductRepo)
        {
            _inMemProductRepo=inMemProductRepo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts(){
            var products=_inMemProductRepo.GetProducts().Select(p=>p.ToProductDTO());
            if (products is not null)
            {
                return Ok(products);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(Guid id){
            var product= _inMemProductRepo.GetProduct(id);
            if (product is not null)
            {
                return Ok(product.ToProductDTO());
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct(CreateProductDTO createProductDTO){
            Product product=new(){
                Id=Guid.NewGuid(),
                Name=createProductDTO.Name,
                Price=createProductDTO.Price,
                CreatedTime=DateTime.Now,
            };
            _inMemProductRepo.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct),new {id=product.Id},product.ToProductDTO());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id,UpdateProductDTO updateProductDTO){
            var product=_inMemProductRepo.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }
            Product updatedProduct= product with{
                Name=updateProductDTO.Name,
                Price=updateProductDTO.Price,
            };
            _inMemProductRepo.UpdateProduct(id,updatedProduct);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id){
            _inMemProductRepo.DeleteProduct(id);
            return NoContent();
        }
    }
}