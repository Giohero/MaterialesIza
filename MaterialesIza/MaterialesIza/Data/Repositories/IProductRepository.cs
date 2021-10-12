namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<SelectListItem> GetComboProduct();

        IQueryable GetAllWithSaleDetails(); 
    }
}
