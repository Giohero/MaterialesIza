using MaterialesIza.Common.Models;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        //// GET: Purchases
        //[HttpGet]
        [HttpGet]
        public IActionResult GetProviders()
        {
            return Ok(this.purchaseRepository.GetAll());
        }


    }

}
