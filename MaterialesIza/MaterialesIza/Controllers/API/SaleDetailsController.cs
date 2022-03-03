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
    public class SaleDetailsController : Controller
    {
        private readonly ISaleDetailRepository saleDetailRepository;

        public SaleDetailsController(ISaleDetailRepository saleDetailRepository)
        {
            this.saleDetailRepository = saleDetailRepository;
        }

        // GET: SaleDetails
        [HttpGet]
        public IActionResult GetSaleDetails()
        {
            //return Ok(this.saleDetailRepository.GetAll());
            var x = this.saleDetailRepository.GetSaleDetails();
            return Ok(x);
        }
    }

}
