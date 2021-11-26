using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class SalesController : Controller
    {
        private readonly ISaleRepository saleRepository;

        public SalesController(ISaleRepository saleRepository)
        {
            this.saleRepository = saleRepository;
        }

        // GET: Sales
        [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(this.saleRepository.GetSale());
        }
    }

}
