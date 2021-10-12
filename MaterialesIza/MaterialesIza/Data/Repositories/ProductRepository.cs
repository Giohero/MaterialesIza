

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetAllWithSaleDetails()
        {
            return this.dataContext.Products
                .Include(p => p.SaleDetails);
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

        
    }
}
