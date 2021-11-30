using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }

        // GET: OrderDetails
        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            return Ok(this.orderDetailRepository.GetOrderDetails());
        }
    }

}
