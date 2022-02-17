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
    }
}
