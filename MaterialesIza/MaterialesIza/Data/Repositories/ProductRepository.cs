﻿

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductRepository : GenericRepository<MaterialesIza.Data.Entities.Product>, IProductRepository
    {
        private readonly DataContext dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetAllWithSaleDetails()
        {
            return this.dataContext.Products
                /*.Include(p => p.SaleDetails)*/;
        }

        public IEnumerable<SelectListItem> GetComboProduct()
        {
            var list = this.dataContext.Products.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un produto)",
                Value = "0"
            });
            return list;
        }

        public MaterialesIza.Common.Models.ProductRequest GetProducts()
        {
            var ps = this.dataContext.Products
                .Include(sd => sd.SaleDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(pt => pt.ProductTypes)
                .FirstOrDefault();

            if (ps == null)
            {
                return null;
            }

            var x = new ProductRequest
            {
                Id = ps.Id,
                Name = ps.Name,
                Price = ps.Price,
                Description = ps.Description,
                ProductTypes = ps.ProductTypes.Name

            };
            return x;

        }

        public MaterialesIza.Common.Models.ProductRequest GetProductWithSalesById(int id)
        {
            var c = this.dataContext.Products
               .Include(s => s.SaleDetails)
               .ThenInclude(sd => sd.Sale)
               .ThenInclude(s => s.Client)
               //.ThenInclude(sd => sd.Product)
               .FirstOrDefault(c => c.Id == id);
            if (c == null)
            {
                return null;
            }
            var x = new ProductRequest
            {
                Id = c.Id,
                Description = c.Description,
                Name = c.Name,
                Price = c.Price,
                SaleDetails = c.SaleDetails?.Select(o => new SaleDetailsRequest

                {
                    Id = o.Id,
                    Quantity = o.Quantity,
                    Sale = new SaleRequest
                    {
                        Id = o.Sale.Id,
                        Date_Sale = o.Sale.Date_Sale,
                        Iva_Sale = o.Sale.Iva_Sale,
                        Sales_Remarks = o.Sale.Sales_Remarks,
                        Total_Sale = o.Sale.Total_Sale,

                        //Client = new ClientRequest
                        //{
                        //    Id = o.Id,
                        //    FirstName = o.User.FirstName,
                        //    LastName = c.User.LastName,
                        //    Email = c.User.Email,
                        //    PhoneNumber = c.User.PhoneNumber,
                        //}


                    }

                }).ToList()
            };
            return x;
        }

    }
}
