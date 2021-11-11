using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class PurchaseRepository: GenericRepository<Purchase>, IPurchaseRepository
    {
        private readonly DataContext dataContext;

        public PurchaseRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetPurchases()
        {
            return this.dataContext.Purchases
                .Include(p => p.Provider.User);
        }
        public IEnumerable<SelectListItem> GetComboPurchase()
        {
            var list = this.dataContext.Products.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una Compra)",
                Value = "0"
            });
            return list;
        }
    }
}
