

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProductTypeRepository : IGenericRepository <MaterialesIza.Data.Entities.ProductType>
    {
        IEnumerable<SelectListItem> GetComboProductType();

        //MaterialesIza.Common.Models.ProductTypeRequest GetProductTypes();
        IQueryable GetProductTypes();
        ProductType GetProductTypeByName(string name);
    }
}
