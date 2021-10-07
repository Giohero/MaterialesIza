
namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    [Route("api/[Controller]")]

    public class ProductsController: Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok (this.productRepository.GetAll());
        }


    }
}
