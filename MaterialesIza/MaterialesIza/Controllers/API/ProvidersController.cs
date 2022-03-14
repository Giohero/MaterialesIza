using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //GET: Providers
        //public IActionResult GetProviderController()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);


        //    }

        //    var emailProvider = new EmailRequest { Email = "alexiscz@gmail.com" };
        //    return Ok(this.providerRepository.GetPurchasesByEmailProvider(emailProvider));
        //}

        public IActionResult GetProviders()
        {
            return Ok(this.providerRepository.GetProviders());
        }

        [HttpPost]
        public async Task<IActionResult> PostProviders([FromBody] MaterialesIza.Common.Models.ProviderRequest providerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityProvider = new MaterialesIza.Data.Entities.Provider
            {
                User = new Data.Entities.User()
                {
                    FirstName = providerRequest.FirstName,
                    LastName = providerRequest.LastName,
                    Email = providerRequest.Email,
                    PhoneNumber = providerRequest.PhoneNumber,
                }
            };

            var newProvider = await this.providerRepository.CreateAsync(entityProvider);
            return Ok(newProvider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProviders([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.ProviderRequest provider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != provider.Id)
            {
                return BadRequest();
            }
            var oldProvider = await this.providerRepository.GetByIdAsync(id);

            if (oldProvider == null)
            {
                return BadRequest("Id no encontrado");
            }
            oldProvider.User.FirstName = provider.FirstName;
            oldProvider.User.LastName = provider.LastName;
            oldProvider.User.Email = provider.Email;
            oldProvider.User.PhoneNumber = provider.PhoneNumber;

            var updateProvider = await this.providerRepository.UpdateAsync(oldProvider);
            return Ok(updateProvider);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldProvider = await this.providerRepository.GetByIdAsync(id);
            if (oldProvider == null)
            {
                return BadRequest("Id no encontrado");
            }

            await this.providerRepository.DeleteAsync(oldProvider);
            return Ok(oldProvider);
        }

    }

}


