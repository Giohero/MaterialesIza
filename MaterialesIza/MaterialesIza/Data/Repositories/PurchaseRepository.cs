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
        public IQueryable GetPurchases()
        {
            return this.dataContext.Purchases
                .Include(o => o.Employee.User)
                .Include(o => o.Provider.User);
        }


        public MaterialesIza.Common.Models.ProviderRequest GetPurchases(EmailRequest emailClient)
        {
            var c = this.dataContext.Providers
                .Include(c => c.User)
                .Include(c => c.Purchases)
                .ThenInclude(o => o.PurchaseDetails)
                //.ThenInclude(od => od.Service)
                //.ThenInclude(s => s.ServiceType)
                .Include(c => c.Purchases)
                .ThenInclude(c => c.Employee)
                .ThenInclude(c => c.User)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailClient.Email);
            if (c == null)
            {
                return null;
            }
            var x = new ProviderRequest
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Email = c.User.Email,
                PhoneNumber = c.User.PhoneNumber,
                Purchases = c.Purchases?.Select(o => new PurchaseRequest
                {
                    Id = o.Id,
                    Employee = new EmployeeRequest
                    {
                        Id = o.Employee.Id,
                        Email = o.Employee.User.Email,
                        FirstName = o.Employee.User.FirstName,
                        LastName = o.Employee.User.LastName,
                        PhoneNumber = o.Employee.User.PhoneNumber,
                    },
                    purchaseDetails = o.PurchaseDetails?.Select(od => new PurchaseDetailsRequest
                    {
                        Id = od.Id,
                        Date_Purchase = od.Date_purchase,
                        Total_Purchase = od.Total_purchase,
                        Iva_Purchase = od.Iva_purchase,
                        Purchase_Remarks = od.Purchase_Remarks


                    }).Where(od => od.Date_Purchase != null).ToList()
                }).ToList()
            };
            return x;
        }


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
                //.Include(t => t.Provider.User)
                .Include(t => t.PurchaseDetails);
        }
    }
}
