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
    public class ClientsController : Controller
    {
        private readonly IClientRepository clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        // GET: Clients
        //[HttpGet]
        //public IActionResult GetClientsController()
        //{
        //  var emailClient = new EmailRequest { Email = "firmalagio@gmail.com" };
        //return Ok(this.clientRepository.GetClientWithOrdersByEmail(emailClient));
        //}

        public IActionResult GetClients()
        {
            return Ok(this.clientRepository.GetClients());
        }

        [HttpPost]
        public async Task<IActionResult> PostClients([FromBody] MaterialesIza.Common.Models.ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityClient = new MaterialesIza.Data.Entities.Client
            {
                User = new Data.Entities.User()
                {
                    FirstName = clientRequest.FirstName,
                    LastName = clientRequest.LastName,
                    Email = clientRequest.Email,
                    PhoneNumber = clientRequest.PhoneNumber,
                }
            };

            var newClient = await this.clientRepository.CreateAsync(entityClient);
            return Ok(newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClients([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.ClientRequest client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != client.Id)
            {
                return BadRequest();
            }
            var oldClient = await this.clientRepository.GetByIdAsync(id);

            if (oldClient == null)
            {
                return BadRequest("Id was not found");
            }
            oldClient.User.FirstName = client.FirstName;
            oldClient.User.LastName = client.LastName;
            oldClient.User.Email = client.Email;
            oldClient.User.PhoneNumber = client.PhoneNumber;

            var updateClient = await this.clientRepository.UpdateAsync(oldClient);
            return Ok(updateClient);

        }

    }

}

