

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

        public MaterialesIza.Common.Models.ProductRequest GetProductWithSalesById(IdRequest IdProduct)
        {
            var c = this.dataContext.Products
               .Include(c => c.SaleDetails)
               .ThenInclude(c => c.Product)
               .ThenInclude(o => o.ProductTypes)
               .Include(c => c.SaleDetails)                           
               .ThenInclude(c => c.Sale)
               .FirstOrDefault(c => c.Id.ToString() == IdProduct.Id);
            if (c == null)
            {
                return null;
            }
            var x = new ProductRequest
            {
                Id = c.Id,
                Name = c.Name,               
                Price = c.Price,
                Description = c.Description,
                SaleDetails = c.SaleDetails?.Select(o => new SaleDetailsRequest
                {
                    Id = o.Id,
                    Quantity = o.Quantity,                  
                    Sale = new SaleRequest
                    {
                        Id = o.Id,
                        Date_Sale = o.Sale.Date_Sale,
                        Total_Sale = o.Sale.Total_Sale,
                        Iva_Sale = o.Sale.Iva_Sale,
                        Sales_Remarks = o.Sale.Sales_Remarks,
                    },
                    Product = new ProductRequest
                    {
                        Id = o.Product.Id,
                        Name = o.Product.Name,
                        Price = o.Product.Price,
                        Description = o.Product.Description,
                        ProductTypes = o.Product.ProductTypes.Name
                    }               
                }).ToList()
            };
            return x;
        }

    }
}
