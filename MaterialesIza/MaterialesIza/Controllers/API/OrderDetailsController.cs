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
            var x = this.orderDetailRepository.GetOrderDetails();
            return Ok(x);
        }
    }

}
