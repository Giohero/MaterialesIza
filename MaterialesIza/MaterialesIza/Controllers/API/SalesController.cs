using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class SalesController : Controller
    {
        private readonly ISaleRepository saleRepository;
        private readonly DataContext dataContext;

        public SalesController(ISaleRepository saleRepository, DataContext dataContext)
        {
            this.saleRepository = saleRepository;
            this.dataContext = dataContext;
        }

        // GET: Sales
       [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(this.saleRepository.GetAll());
        }
        
    }

}
