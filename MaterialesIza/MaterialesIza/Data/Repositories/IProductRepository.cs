namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<MaterialesIza.Data.Entities.Product>
    {
        IEnumerable<SelectListItem> GetComboProduct();

        IQueryable GetAllWithSaleDetails();

        MaterialesIza.Common.Models.ProductRequest GetProducts();
    }
}
