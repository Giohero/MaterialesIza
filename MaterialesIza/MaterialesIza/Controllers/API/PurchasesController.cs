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
        public IActionResult GetPurchaseController()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

                
            }

            var emailApplicant = new EmailRequest { Email = "alexiscz@gmail.com" };
            return Ok(this.purchaseRepository.GetPurchases(emailApplicant));
        }
    }

}
