using MaterialesIza.Data.Entities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaterialesIza.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetOrders()
        {
            return this.dataContext.Orders
                .Include(p => p.Client.User)
                .Include(p => p.Employee.User);
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
