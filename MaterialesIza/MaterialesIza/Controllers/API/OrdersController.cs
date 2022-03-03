using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly DataContext dataContext;

        public OrdersController(IOrderRepository orderRepository,DataContext dataContext)
        {
            this.orderRepository = orderRepository;
            this.dataContext = dataContext;
        }

        // GET: Orders
        //[HttpPost]
        //[Route("GetOrdersByEmailAsync")]
        public IActionResult GetOrdersController()
        {         
            return Ok(this.orderRepository.GetOrders());
        }
    }

}
