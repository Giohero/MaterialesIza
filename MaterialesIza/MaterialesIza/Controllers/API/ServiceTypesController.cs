namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

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
            return Ok(this.serviceTypeRepository.GetAll());
        }
    }
}
