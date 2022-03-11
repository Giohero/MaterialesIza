
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]

    public class ServicesController : Controller
    {
        private readonly IServiceRepository serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(this.serviceRepository.GetServices());
        }

        [HttpPost]
        public async Task<IActionResult> PostServices([FromBody] MaterialesIza.Common.Models.ServiceRequest service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityService = new MaterialesIza.Data.Entities.Service
            {
                Name = service.Name,
                Description = service.Description

            };
            var newService = await this.serviceRepository.CreateAsync(entityService);
            return Ok(newService);
        }

    }
}
