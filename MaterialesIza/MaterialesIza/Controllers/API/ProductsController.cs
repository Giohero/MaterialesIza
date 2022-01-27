
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityProduct = new MaterialesIza.Data.Entities.Product
            {
                Name = product.Name
            };

            var newProductType = await this.productRepository.CreateAsync(entityProduct);
            return Ok(newProductType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.Id)
            {
                return BadRequest();
            }
            var oldProduct = await this.productRepository.GetByIdAsync(id);

            if (oldProduct == null)
            {
                return BadRequest("Id was not found");
            }
            oldProduct.Name = product.Name;
            var updateProductType = await this.productRepository.UpdateAsync(oldProduct);
            return Ok(updateProductType);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldProduct = await this.productRepository.GetByIdAsync(id);

            if (oldProduct == null)
            {
                return BadRequest("Id was not found");
            }
            await this.productRepository.DeleteAsync(oldProduct);
            return Ok(oldProduct);
        }
    }
}
