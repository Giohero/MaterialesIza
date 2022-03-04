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
        private readonly DataContext dataContext;

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
    }

}
