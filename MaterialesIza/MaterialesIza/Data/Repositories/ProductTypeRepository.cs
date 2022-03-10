

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductTypeRepository : GenericRepository<MaterialesIza.Data.Entities.ProductType>, IProductTypeRepository
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

        //public MaterialesIza.Common.Models.ProductTypeRequest GetProductTypes()
        //{
        //    var a = this.dataContext.ProductTypes.FirstOrDefault();

        //    if (a == null)
        //    {
        //        return null;
        //    }

        //    var x = new ProductTypeRequest
        //    {
        //        Id = a.Id,
        //        Name = a.Name
        //    };
        //    return x;
        //}
        public IQueryable GetProductTypes()
        {
            return this.dataContext.ProductTypes;
        }

    }
}
