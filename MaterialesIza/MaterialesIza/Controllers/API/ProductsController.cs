
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Entities;
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]

    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IProductTypeRepository productTypeRepository;


        public ProductsController(IProductRepository productRepository,IProductTypeRepository productTypeRepository)
        {
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAllProductsWithType());
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]MaterialesIza.Common.Models.ProductRequest product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productType = this.productTypeRepository.GetProductTypeByName(product.ProductTypes);
            if (productType == null)
            {
                return BadRequest("product type not found");
            }

            var entityProduct = new MaterialesIza.Data.Entities.Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,

                //
                ProductTypes = productType //asi no agrega el tipo de producto
            };

            var newProduct = await this.productRepository.CreateAsync(entityProduct);
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.ProductRequest product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.Id)
            {
                return BadRequest();
            }
            //
            var productType = this.productTypeRepository.GetProductTypeByName(product.ProductTypes);

            var oldProduct = await this.productRepository.GetByIdAsync(id);

            if (oldProduct == null)
            {
                return BadRequest("Id was not found");
            }
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            //
            oldProduct.ProductTypes = productType;
            var updateProduct = await this.productRepository.UpdateAsync(oldProduct);
            return Ok(updateProduct);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
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
