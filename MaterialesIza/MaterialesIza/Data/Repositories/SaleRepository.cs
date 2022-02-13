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

        public MaterialesIza.Common.Models.EmployeeRequest GetSale(EmailRequest emailEmployee)
        {
            var c = this.dataContext.Employees
                .Include(c => c.User)
                .Include(c => c.Sales)
                .ThenInclude(o => o.SaleDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(s => s.ProductTypes)
                .Include(c => c.Sales)
                .ThenInclude(c => c.Client)
                .ThenInclude(c => c.User)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailEmployee.Email);
            if (c == null)
            {
                return null;
            }
            var x = new EmployeeRequest
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Email = c.User.Email,
                PhoneNumber = c.User.PhoneNumber,
                Sales = c.Sales?.Select(o => new SaleRequest
                {
                    Id = o.Id,
                    Client = new ClientRequest
                    {
                        Id = o.Client.Id,
                        Email = o.Client.User.Email,
                        FirstName = o.Client.User.FirstName,
                        LastName = o.Client.User.LastName,
                        PhoneNumber = o.Client.User.PhoneNumber,
                    },
                    SaleDetails = o.SaleDetails?.Select(od => new SaleDetailsRequest
                    {
                        Id = od.Id,
                        Date_Sale = od.Date_Sale,
                        Total_Sale = od.Total_Sale,
                        Iva_Sale = od.Iva_Sale,
                        Sales_Remarks = od.Sales_Remarks,
                        product = new ProductRequest
                        {

                            Id = od.Product.Id,
                            Name = od.Product.Name,
                            Price = od.Product.Price,
                            Quantity = od.Product.Quantity,
                            Description = od.Product.Description,
                            ProductTypes = od.Product.ProductTypes.Name

                        }

                    }).Where(od => od.Date_Sale != null).ToList()
                }).ToList()
            };

            return x;
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


