﻿using MaterialesIza.Common.Models;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaterialesIza.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class ProvidersController : Controller
    {
        private readonly IProviderRepository providerRepository;

        public ProvidersController(IProviderRepository providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        // GET: Providers
        

        public IActionResult GetProviderController()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);


            }

            var emailProvider = new EmailRequest { Email = "alexiscz@gmail.com" };
            return Ok(this.providerRepository.GetPurchasesByEmailProvider(emailProvider));
        }
    }

}
