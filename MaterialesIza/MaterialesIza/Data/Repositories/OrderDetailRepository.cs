using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly DataContext dataContext;

        public OrderDetailRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetOrderDetails()
        {
            return this.dataContext.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(o => o.Client)
                .ThenInclude(c => c.User)
                .Include(od => od.Service);
                //.Where(od => od.Date_Order != null);
        }
        public IEnumerable<SelectListItem> GetComboOrderDetails()
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
