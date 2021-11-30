using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class PurchaseDetailRepository: GenericRepository<PurchaseDetail>, IPurchaseDetailRepository
    {
        private readonly DataContext dataContext;

        public PurchaseDetailRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetPurchaseDetails()
        {
            return this.dataContext.PurchaseDetails
                //.Include(p => p.Purchase)
                .Include(p => p.Product);
        }
        public IEnumerable<SelectListItem> GetComboPurchaseDetails()
        {
            var list = this.dataContext.Products.Select(m => new SelectListItem
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
