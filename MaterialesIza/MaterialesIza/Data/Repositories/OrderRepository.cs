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

        public MaterialesIza.Common.Models.OrderRequest GetOrders()
        {
            var o = this.dataContext.Orders
               .Include(od => od.OrderDetails)
               .ThenInclude(s => s.Service)
               .ThenInclude(st => st.ServiceType)
               .FirstOrDefault();
            if (o == null)
            {
                return null;
            }
            var x = new OrderRequest
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


                })/*.Where(od => od.Date_Sale != null)*/.ToList()
            };

            return x;
        }
    }
}
