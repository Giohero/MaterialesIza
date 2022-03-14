﻿using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Entities;
using MaterialesIza.Data.Repositories;
using MaterialesIza.Helpers;
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
        private readonly IUserHelper userHelper;

        public ClientsController(IClientRepository clientRepository, IUserHelper userHelper)
        {
            this.clientRepository = clientRepository;
            this.userHelper = userHelper;
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
            var userClient = await this.userHelper.GetUserByEmailAsync(clientRequest.Email);
            if(userClient == null)
            {
                userClient = new Data.Entities.User()
                {
                    FirstName = clientRequest.FirstName,
                    LastName = clientRequest.LastName,
                    Email = clientRequest.Email,
                    PhoneNumber = clientRequest.PhoneNumber,
                };
                string pasword = "123456";
                var result = await this.userHelper.AddUserAsync(userClient, pasword);
            }

            var emailClient = new EmailRequest { Email = clientRequest.Email };
            var oldClient = this.clientRepository.GetClientWithOrdersByEmail(emailClient);
            if (oldClient != null)
            {
                return BadRequest("Ya existe el usuario");
            }
            var entityClient = new Client
            {
                User = userClient
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
            var oldClient = await this.userHelper.GetUserByEmailAsync(client.Email);

            if (oldClient == null)
            {
                return BadRequest("Id was not found");
            }
            oldClient.FirstName = client.FirstName;
            oldClient.LastName = client.LastName;
            oldClient.Email = client.Email;
            oldClient.PhoneNumber = client.PhoneNumber;

            var updateClient = await this.userHelper.UpdateUserAsync(oldClient);
            return Ok(updateClient);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldClient = await this.clientRepository.GetByIdAsync(id);
            if (oldClient == null)
            {
                return BadRequest("Id not found");
            }

            await this.clientRepository.DeleteAsync(oldClient);
            return Ok(oldClient);
        }

    }

}

