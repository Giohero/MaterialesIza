using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class SaleDetailRepository: GenericRepository<SaleDetail>, ISaleDetailRepository
    {
        private readonly DataContext dataContext;

        public SaleDetailRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetSaleDetails()
        {
            return this.dataContext.SaleDetails
                .Include(p => p.Sale)
                .Include(p => p.Product);
        }
        public IEnumerable<SelectListItem> GetComboSaleDetails()
        {
            var list = this.dataContext.Services.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una Venta detalle)",
                Value = "0"
            });
            return list;
        }
    }
}
