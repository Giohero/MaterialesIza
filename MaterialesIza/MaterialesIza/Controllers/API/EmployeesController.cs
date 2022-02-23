using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Controllers.API
{
    [Route("api/[Controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly DataContext dataContext;

        public EmployeesController(IEmployeeRepository employeeRepository, DataContext dataContext)
        {
            this.employeeRepository = employeeRepository;
            this.dataContext = dataContext;
        }

        // GET: Employees
        public IActionResult GetEmployeesController()
        {
            //return Ok(this.employeeRepository.GetAll());

            var emailEmployee = new EmailRequest { Email = "jesus.Sal@gmail.com" };
            return Ok(this.employeeRepository.GetEmployeeWithOrdersByEmail(emailEmployee));
        }
    }

}
