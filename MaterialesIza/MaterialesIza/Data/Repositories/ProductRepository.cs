

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
                Quantity = ps.Quantity,
                Description = ps.Description,
                ProductTypes = ps.ProductTypes.Name

            };
            return x;

        }

    }
}
