using MaterialesIza.Common.Models;
using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class SaleRepository : GenericRepository<MaterialesIza.Data.Entities.Sale>, ISaleRepository
    {
        private readonly DataContext dataContext;

        public SaleRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetSale()
        {
            return this.dataContext.Sales
                .Include(p => p.Client.User)
                .Include(p => p.Employee.User);
        }

        public IEnumerable<SelectListItem> GetComboSale()
        {
            var list = this.dataContext.Products.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una venta)",
                Value = "0"
            });
            return list;
        }
    }
}


