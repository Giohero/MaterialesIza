
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

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
