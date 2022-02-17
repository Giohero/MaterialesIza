﻿
namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ProviderRepository : GenericRepository<MaterialesIza.Data.Entities.Provider>, IProviderRepository
    {
        private readonly DataContext dataContext;

        public ProviderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetProviders()
        {
            return this.dataContext.Providers
                .Include(p => p.User);
        }
        public IEnumerable<SelectListItem> GetComboProvider()
        {
            var list = this.dataContext.Providers.Select(m => new SelectListItem
            {
                Text = m.User.FullName,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un Proveedor)",
                Value = "0"
            });
            return list;
        }



        public MaterialesIza.Common.Models.ProviderRequest GetPurchasesByEmailProvider(EmailRequest emailProvider)
        {
            var c = this.dataContext.Providers
                .Include(c => c.User)
                .Include(c => c.Purchases)
                .ThenInclude(o => o.PurchaseDetails)
                .Include(c => c.Purchases)
                .ThenInclude(c => c.Employee)
                .ThenInclude(c => c.User)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailProvider.Email);
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
                    PurchaseDetails = o.PurchaseDetails?.Select(od => new PurchaseDetailsRequest
                    {
                        Id = od.Id,
                        Total_Purchase = od.Total_purchase,
                        Iva_Purchase = od.Iva_purchase,
                        Purchase_Remarks = od.Purchase_Remarks


                    }).Where(od => od.Date_Purchase != null).ToList()
                }).ToList()
            };
            return x;
        }

    }
}
