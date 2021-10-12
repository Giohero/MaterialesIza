using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    using MaterialesIza.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]

    public class ProductTypesController :Controller
    {
        private readonly IProductTypeRepository productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult GetProductTypes()
        {
            return Ok(this.productTypeRepository.GetAll());
        }

    }
}
