
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
            return Ok(this.serviceRepository.GetAll());
        }

    }
}
