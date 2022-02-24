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

        public IEnumerable<OrderRequest> GetOrders()
        {
            var o = this.dataContext.Orders
                     .Include(e => e.Employee.User)
                     .Include(od => od.OrderDetails)
                     .ThenInclude(s => s.Service)
                     .ThenInclude(st => st.ServiceType);

            if (o == null)
            {
                return null;
            }

            var x = o.Select(or => new OrderRequest
            {
                Id = or.Id,
                Date_Order = or.Date_Order,
                Total_Order = or.Total_Order,
                Iva_Order = or.Iva_Order,
                Order_Remarks = or.Order_Remarks,

                //Employee = new EmployeeRequest
                //{
                //    Id = or.Id,
                //    FirstName = or.Employee.User.FirstName,
                //    LastName = or.Employee.User.LastName,
                //    Email = or.Employee.User.Email,
                //    PhoneNumber = or.Employee.User.PhoneNumber,
                //},

                OrderDetails = or.OrderDetails.Select(odr => new OrderDetailsRequest
                {
                    Id = odr.Id,
                    Quantity = odr.Quantity,
                    Price = odr.Price,
                    Service = new ServiceRequest
                    {
                        Id = odr.Service.Id,
                        Name = odr.Service.Name,
                        Description = odr.Service.Description,
                        ServiceType = odr.Service.ServiceType.TypeService
                    }
                }).ToList()
            }).ToList();

            return x;
        }
    }
}
