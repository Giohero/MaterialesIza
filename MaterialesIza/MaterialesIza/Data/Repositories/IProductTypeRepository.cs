

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface IProductTypeRepository : IGenericRepository <ProductType>
    {
        IEnumerable<SelectListItem> GetComboProductType();
    }
}
