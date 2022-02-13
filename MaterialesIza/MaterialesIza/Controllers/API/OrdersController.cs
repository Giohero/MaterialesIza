using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
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
            var emailClient = new EmailRequest { Email = "firmalagio@gmail.com" };
            return Ok(this.orderRepository.GetOrders(emailClient));
        }
    }

}
