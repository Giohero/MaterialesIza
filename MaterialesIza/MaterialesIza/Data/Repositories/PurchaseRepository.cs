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
    public class PurchaseRepository: GenericRepository<MaterialesIza.Data.Entities.Purchase>, IPurchaseRepository
    {
        private readonly DataContext dataContext;

        public PurchaseRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        //public IQueryable GetPurchases()
        //{
        //    return this.dataContext.Purchases
        //        .Include(o => o.Employee.User)
        //        .Include(o => o.Provider.User);
        //}



        public IEnumerable<SelectListItem> GetComboPurchase()
        {
            var list = this.dataContext.Purchases.Select(m => new SelectListItem
            {
                //Text = m.Provider.User.FullName,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un proveedor)",
                Value = "0"
            });
            return list;
        }
        public IQueryable GetPurchaseWithProvider()
        {
            return this.dataContext.Purchases
                .Include(t => t.Provider.User)
                .Include(t => t.PurchaseDetails);
        }

        public IQueryable GetPurchases()
        {
            return this.dataContext.Purchases;
               
        }

        public IEnumerable<PurchaseRequest> GetAllPurchases()
        {
            var p = this.dataContext.Purchases;
            if(p==null)
            {
                return null;
            }

            var x = p.Select(pr => new PurchaseRequest
            {
                Id = pr.Id,
                Date_purchase = pr.Date_purchase,
                Iva_purchase = pr.Iva_purchase,
                Total_purchase = pr.Total_purchase,
                Purchase_Remarks = pr.Purchase_Remarks
            }).ToList();
            return x;
        }



    }
}
