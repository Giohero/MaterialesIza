

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeRepository : GenericRepository<MaterialesIza.Data.Entities.Employee>, IEmployeeRepository
    {
        private readonly DataContext dataContext;

        public EmployeeRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetEmployee()
        {
            return this.dataContext.Employees;
                //.Include(e => e.User);
        }

        public IEnumerable<EmployeeRequest> GetEmployees()
        {
            var i = this.dataContext.Employees
                .Include(i => i.User);

            if (i == null)
            {
                return null;
            }

            var x = i.Select(a => new EmployeeRequest
            {
                Id = a.Id,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber
            }).ToList();

            return x;

        }

        public IEnumerable<SelectListItem> GetComboEmployee()
        {
            var list = this.dataContext.Employees.Select(e => new SelectListItem
            {
                Text = e.User.FullName,
                Value = $"{e.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un empleado)",
                Value = "0"
            });
            return list;
        }
        
        public MaterialesIza.Common.Models.EmployeeRequest GetEmployeeWithOrdersByEmail(EmailRequest emailEmployee)
        {
            var c = this.dataContext.Employees
                .Include(c => c.User)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Service)
                .ThenInclude(s => s.ServiceType)
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailEmployee.Email);
            if (c == null)
            {
                return null;
            }
            var x = new EmployeeRequest
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Email = c.User.Email,
                PhoneNumber = c.User.PhoneNumber,
                Orders = c.Orders?.Select(o => new OrderRequest
                {
                    Id = o.Id,
                    Date_Order = o.Date_Order,
                    Total_Order = o.Total_Order,
                    Iva_Order = o.Iva_Order,
                    Order_Remarks = o.Order_Remarks,
                    OrderDetails = o.OrderDetails?.Select(od => new OrderDetailsRequest
                    {
                        Id = od.Id,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Service = new ServiceRequest
                        {
                            Id = od.Service.Id,
                            Name = od.Service.Name,
                            Description = od.Service.Description,
                            ServiceType = od.Service.ServiceType.TypeService
                        }
                    }).ToList()
                }).ToList()
            };
            return x;
        }
    }
}
