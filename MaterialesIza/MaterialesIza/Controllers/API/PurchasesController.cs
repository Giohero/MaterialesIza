using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseRepository purchaseRepository;

        public PurchasesController(IPurchaseRepository purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        // GET: Purchases
        [HttpGet]
        public IActionResult GetPurchases()
        {
            return Ok(this.purchaseRepository.GetAll());
        }
    }

}
