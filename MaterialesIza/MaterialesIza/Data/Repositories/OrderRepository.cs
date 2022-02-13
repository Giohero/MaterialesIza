using MaterialesIza.Data.Entities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MaterialesIza.Common.Models;

namespace MaterialesIza.Data.Repositories
{
    public class OrderRepository : GenericRepository<MaterialesIza.Data.Entities.Order>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetOrder()
        {
            return this.dataContext.Orders
                .Include(o => o.Client.User)
                .Include(o => o.Employee.User);
        }
        public MaterialesIza.Common.Models.ClientRequest GetOrders(EmailRequest emailClient)
        {
            var c = this.dataContext.Clients
                .Include(c => c.User)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Service)
                .ThenInclude(s => s.ServiceType)
                .Include(c => c.Orders)
                .ThenInclude(c => c.Employee)
                .ThenInclude(c => c.User)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailClient.Email);
            if (c == null)
            {
                return null;
            }
            var x = new ClientRequest
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Email = c.User.Email,
                PhoneNumber = c.User.PhoneNumber,
                Orders = c.Orders?.Select(o => new OrderRequest
                {
                    Id = o.Id,
                    Employee = new EmployeeRequest
                    {
                        Id = o.Employee.Id,
                        Email = o.Employee.User.Email,
                        FirstName = o.Employee.User.FirstName,
                        LastName = o.Employee.User.LastName,
                        PhoneNumber = o.Employee.User.PhoneNumber,
                    },
                    OrderDetails = o.OrderDetails?.Select(od => new OrderDetailsRequest
                    {
                        Id = od.Id,
                        Date_Order = od.Date_Order,
                        Total_Order = od.Total_Order,
                        Iva_Order = od.Iva_Order,
                        Order_Remarks = od.Order_Remarks,
                        Service = new ServiceRequest
                        {

                            Id = od.Service.Id,
                            Name = od.Service.Name,
                            Description = od.Service.Description,
                            ServiceType = od.Service.ServiceType.TypeService

                        }

                    }).Where(od => od.Date_Order != null).ToList()
                }).ToList()
            };

            return x;
        }

        public IEnumerable<SelectListItem> GetComboOrder()
        {
            var list = this.dataContext.Services.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una Orden)",
                Value = "0"
            });
            return list;
        }
    }
}
