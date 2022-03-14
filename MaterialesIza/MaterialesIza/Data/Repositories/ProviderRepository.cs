
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

        public IQueryable GetProvidersWithUser()
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

        public IEnumerable<ProviderRequest> GetProviders()
        {
            var i = this.dataContext.Providers
                .Include(i => i.User);

            if (i == null)
            {
                return null;
            }

            var x = i.Select(a => new ProviderRequest
            {
                Id = a.Id,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber
            }).ToList();

            return x;

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
                    Date_purchase = o.Date_purchase,
                    Total_purchase = o.Total_purchase,
                    Iva_purchase = o.Iva_purchase,
                    Purchase_Remarks = o.Purchase_Remarks,
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
                        Quantity = od.Quantity,
                        
                        Product = new ProductRequest
                        {

                            Id = od.Product.Id,
                            Name = od.Product.Name,
                            Price = od.Product.Price,
                            Description = od.Product.Description,
                            ProductTypes = od.Product.ProductTypes.Name

                        }


                    })/*.Where(od => od.Date_Purchase != null)*/.ToList()
                }).ToList()
            };
            return x;
        }

    }
}
