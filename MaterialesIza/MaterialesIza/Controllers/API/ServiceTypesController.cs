namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            return Ok(this.serviceTypeRepository.GetAll());
        }
    }
}
