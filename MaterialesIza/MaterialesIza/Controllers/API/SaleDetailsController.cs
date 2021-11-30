using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
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
            return Ok(this.saleDetailRepository.GetSaleDetails());
        }
    }

}
