namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[Controller]")]

    public class ServiceTypesController : Controller
    {
        private readonly IServiceTypeRepository serviceTypeRepository;

        public ServiceTypesController(IServiceTypeRepository serviceTypeRepository)
        {
            this.serviceTypeRepository = serviceTypeRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetServiceTypes()
        {
            return Ok(this.serviceTypeRepository.GetServiceTypes());
        }

        [HttpPost]
        public async Task<IActionResult> PostServiceTypes([FromBody] MaterialesIza.Common.Models.ServiceTypeRequest serviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityServiceType = new MaterialesIza.Data.Entities.ServiceType
            {
                TypeService = serviceType.TypeService
            };

            var newServiceType = await this.serviceTypeRepository.CreateAsync(entityServiceType);
            return Ok(newServiceType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceTypes([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.ServiceTypeRequest serviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != serviceType.Id)
            {
                return BadRequest();
            }
            var oldServiceType = await this.serviceTypeRepository.GetByIdAsync(id);

            if (oldServiceType == null)
            {
                return BadRequest("Id was not found");
            }
            oldServiceType.TypeService = serviceType.TypeService;
            var updateServiceType = await this.serviceTypeRepository.UpdateAsync(oldServiceType);
            return Ok(updateServiceType);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldServiceType = await this.serviceTypeRepository.GetByIdAsync(id);

            if (oldServiceType == null)
            {
                return BadRequest("Id was not found");
            }
            await this.serviceTypeRepository.DeleteAsync(oldServiceType);
            return Ok(oldServiceType);
        }
    }
}
