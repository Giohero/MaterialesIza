namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]

    public class ProductTypesController : Controller
    {
        private readonly IProductTypeRepository productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetProductTypes()
        {
            return Ok(this.productTypeRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostProductTypes([FromBody] ProducType producType)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityProductTypes = new MaterialesIza.Data.Entities.ProductType
            {
                Name = producType.Name
            };

            var newProductType = await this.productTypeRepository.CreateAsync(entityProductTypes);
            return Ok(newProductType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductTypes([FromRoute] int id, [FromBody]MaterialesIza.Common.Models.ProducType producType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id!= producType.Id)
            {
                return BadRequest();
            }
            var oldProductType = await this.productTypeRepository.GetByIdAsync(id);

            if(oldProductType == null)
            {
                return BadRequest("Id was not found");
            }
            oldProductType.Name = producType.Name;
            var updateProductType = await this.productTypeRepository.UpdateAsync(oldProductType);
            return Ok(updateProductType);
            
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteProductTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
   
            var oldProductType = await this.productTypeRepository.GetByIdAsync(id);

            if (oldProductType == null)
            {
                return BadRequest("Id was not found");
            }
            await this.productTypeRepository.DeleteAsync(oldProductType);
            return Ok(oldProductType);
        }

    }
}
