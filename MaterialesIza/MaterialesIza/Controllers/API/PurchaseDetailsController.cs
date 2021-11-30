using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class PurchaseDetailsController : Controller
    {
        private readonly IPurchaseDetailRepository purchaseDetailRepository;

        public PurchaseDetailsController(IPurchaseDetailRepository purchaseDetailRepository)
        {
            this.purchaseDetailRepository = purchaseDetailRepository;
        }

        // GET: PurchaseDetails
        [HttpGet]
        public IActionResult GetPurchaseDetails()
        {
            return Ok(this.purchaseDetailRepository.GetPurchaseDetails());
        }
    }

}
