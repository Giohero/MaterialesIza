
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]

    public class ServicesController : Controller
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;

        public ServicesController(IServiceRepository serviceRepository,IServiceTypeRepository serviceTypeRepository)
        {
            this.serviceRepository = serviceRepository;
            this.serviceTypeRepository = serviceTypeRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(this.serviceRepository.GetAllServicessWithType());
        }

        [HttpPost]
        public async Task<IActionResult> PostService([FromBody] MaterialesIza.Common.Models.ServiceRequest service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serviceType = this.serviceTypeRepository.GetServiceTypeByName(service.ServiceType);
            if (serviceType == null)
            {
                return BadRequest("servicetype not found");
            }
            var entityService = new MaterialesIza.Data.Entities.Service
            {
                Name = service.Name,
                Description = service.Description
                

            };
            var newService = await this.serviceRepository.CreateAsync(entityService);
            return Ok(newService);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.ServiceRequest service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != service.Id)
            {
                return BadRequest();
            }
            var oldService = await this.serviceRepository.GetByIdAsync(id);

            if (oldService == null)
            {
                return BadRequest("Id was not found");
            }
            oldService.Name = service.Name;
            oldService.Description = service.Description;
            var updateProduct = await this.serviceRepository.UpdateAsync(oldService);
            return Ok(updateProduct);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldService = await this.serviceRepository.GetByIdAsync(id);

            if (oldService == null)
            {
                return BadRequest("Id was not found");
            }
            await this.serviceRepository.DeleteAsync(oldService);
            return Ok(oldService);
        }

    }
}
