using MaterialesIza.Data.Entities;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IActionResult GetPurchases()
        {
            return Ok(this.purchaseRepository.GetPurchases());
        }

        [HttpPost]
        public async Task<IActionResult> PostPurchases([FromBody] MaterialesIza.Common.Models.PurchaseRequest purchaseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityPurchase = new MaterialesIza.Data.Entities.Purchase
            {
              Date_purchase=purchaseRequest.Date_purchase,
              Total_purchase=purchaseRequest.Total_purchase,
              Iva_purchase=purchaseRequest.Iva_purchase,
              Purchase_Remarks=purchaseRequest.Purchase_Remarks
                
            };

            var newPurchase = await this.purchaseRepository.CreateAsync(entityPurchase);
            return Ok(newPurchase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.PurchaseRequest purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != purchase.Id)
            {
                return BadRequest();
            }
            var oldPurchase = await this.purchaseRepository.GetByIdAsync(id);

            if (oldPurchase == null)
            {
                return BadRequest("Id no encontrado");
            }
            oldPurchase.Date_purchase = purchase.Date_purchase;
            oldPurchase.Total_purchase = purchase.Total_purchase;
            oldPurchase.Iva_purchase = purchase.Iva_purchase;
            oldPurchase.Purchase_Remarks = purchase.Purchase_Remarks;

            var updatePurchase = await this.purchaseRepository.UpdateAsync(oldPurchase);
            return Ok(updatePurchase);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldPurchase = await this.purchaseRepository.GetByIdAsync(id);
            if (oldPurchase == null)
            {
                return BadRequest("Id no encontrado");
            }

            await this.purchaseRepository.DeleteAsync(oldPurchase);
            return Ok(oldPurchase);
        }


    }

}
