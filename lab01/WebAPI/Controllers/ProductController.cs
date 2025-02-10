using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.impl;
using Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetCategories() => repository.GetProducts();

        [HttpGet("id")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);

        [HttpPost]
        public IActionResult PostProduct(Product Product)
        {
            repository.SaveProduct(Product);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var c = repository.GetProductById(id);
            if (c == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(c);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult PutProduct(int id, Product Product)
        {
            var cTmp = repository.GetProductById(id);
            if (cTmp == null)
            {
                return NotFound();
            }

            cTmp.ProductName = Product.ProductName;

            repository.UpdateProduct(cTmp);
            return NoContent();
        }
    }
}
