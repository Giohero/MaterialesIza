using MaterialesIza.Common.Models;
using MaterialesIza.Data;
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
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        // GET: Employees
        //public IActionResult GetEmployeesController()
        //{
        //    //return Ok(this.employeeRepository.GetAll());

        //    var emailEmployee = new EmailRequest { Email = "jesus.Sal@gmail.com" };
        //    return Ok(this.employeeRepository.GetEmployeeWithOrdersByEmail(emailEmployee));
        //}

        public IActionResult GetEmployees()
        {
            return Ok(this.employeeRepository.GetEmployees());
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployees([FromBody] MaterialesIza.Common.Models.EmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityEmployee = new MaterialesIza.Data.Entities.Employee
            {
                User = new Data.Entities.User()
                {
                    FirstName = employeeRequest.FirstName,
                    LastName = employeeRequest.LastName,
                    Email = employeeRequest.Email,
                    PhoneNumber = employeeRequest.PhoneNumber,
                }
            };

            var newEmployee = await this.employeeRepository.CreateAsync(entityEmployee);
            return Ok(newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees([FromRoute] int id, [FromBody] MaterialesIza.Common.Models.EmployeeRequest employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != employee.Id)
            {
                return BadRequest();
            }
            var oldEmployee = await this.employeeRepository.GetByIdAsync(id);

            if (oldEmployee == null)
            {
                return BadRequest("Id was not found");
            }
            oldEmployee.User.FirstName = employee.FirstName;
            oldEmployee.User.LastName = employee.LastName;
            oldEmployee.User.Email = employee.Email;
            oldEmployee.User.PhoneNumber = employee.PhoneNumber;

            var updateEmployee = await this.employeeRepository.UpdateAsync(oldEmployee);
            return Ok(updateEmployee);

        }
    }

}
