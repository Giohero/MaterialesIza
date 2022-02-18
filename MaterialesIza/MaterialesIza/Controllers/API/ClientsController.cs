using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{

    [Route("api/[Controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository clientRepository;
        private readonly DataContext dataContext;

        public ClientsController(IClientRepository clientRepository, DataContext dataContext)
        {
            this.clientRepository = clientRepository;
            this.dataContext = dataContext;
        }

        // GET: Clients
        //[HttpGet]
        public IActionResult GetClientsController()
        {
            var emailClient = new EmailRequest { Email = "firmalagio@gmail.com" };
            return Ok(this.clientRepository.GetClientWithOrdersByEmail(emailClient));
        }
    }

}
