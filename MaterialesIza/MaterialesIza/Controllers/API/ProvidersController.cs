using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class ProvidersController : Controller
    {
        private readonly IProviderRepository providerRepository;

        public ProvidersController(IProviderRepository providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        // GET: Providers
        [HttpGet]
        public IActionResult GetProviders()
        {
            return Ok(this.providerRepository.GetAll());
        }
    }

}
