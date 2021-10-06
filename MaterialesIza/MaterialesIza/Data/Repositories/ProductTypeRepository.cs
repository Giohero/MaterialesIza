﻿

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        private readonly DataContext dataContext;

        public ProductTypeRepository(DataContext dataContext):base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboProductType()
        {
            var list = this.dataContext.ProductTypes.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un tipo de producto)",
                Value = "0"
            });
            return list;

        }

    }
}
