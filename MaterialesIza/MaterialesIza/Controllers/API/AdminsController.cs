using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class AdminsController : Controller
    {
        private readonly IAdminRepository adminRepository;

        public AdminsController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        // GET: Admins
        [HttpGet]
        public IActionResult GetAdmins()
        {
            return Ok(this.adminRepository.GetAdmins());
        }
    }
}
