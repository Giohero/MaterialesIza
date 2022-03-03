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
