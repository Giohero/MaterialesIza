using MaterialesIza.Common.Models;
using MaterialesIza.Data;
using MaterialesIza.Data.Entities;
using MaterialesIza.Data.Repositories;
using MaterialesIza.Helpers;
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
        private readonly IUserHelper userHelper;


        public EmployeesController(IEmployeeRepository employeeRepository, IUserHelper userHelper)
        {
            this.employeeRepository = employeeRepository;
            this.userHelper = userHelper;
        }

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
            var userEmployee = await this.userHelper.GetUserByEmailAsync(employeeRequest.Email);
            if (userEmployee == null)
            {
                userEmployee = new Data.Entities.User
                {
                    FirstName = employeeRequest.FirstName,
                    LastName = employeeRequest.LastName,
                    Email = employeeRequest.Email,
                    UserName = employeeRequest.Email,
                    PhoneNumber = employeeRequest.PhoneNumber,
                };

                string pasword = "123456";
                var result = await this.userHelper.AddUserAsync(userEmployee, pasword);

                await this.userHelper.AddUserToRoleAsync(userEmployee, "Employee");
            }

            var emailEmployee = new EmailRequest { Email = employeeRequest.Email };
            var oldEmployee = this.employeeRepository.GetEmployeeWithOrdersByEmail(emailEmployee);
            if (oldEmployee != null)
            {
                return BadRequest("El usuario ya existe");
            }
            var entityEmployee = new Employee
            {
                User = userEmployee
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
            var userEmployee = await this.userHelper.GetUserByEmailAsync(employee.Email);

            if (userEmployee == null)
            {
                return BadRequest("Id was not found");
            }
            userEmployee.FirstName = employee.FirstName;
            userEmployee.LastName = employee.LastName;
            userEmployee.Email = employee.Email;
            userEmployee.PhoneNumber = employee.PhoneNumber;

            var updateEmployee = await this.userHelper.UpdateUserAsync(userEmployee);
            return Ok(updateEmployee);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldEmployee = await this.employeeRepository.GetByIdAsync(id);
            if (oldEmployee == null)
            {
                return BadRequest("Id not found");
            }

            await this.employeeRepository.DeleteAsync(oldEmployee);
            return Ok(oldEmployee);
        }
    }

}
