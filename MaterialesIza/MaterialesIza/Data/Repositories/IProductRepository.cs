namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<SelectListItem> GetComboProduct();

        IQueryable GetAllWithSaleDetails();

        //MaterialesIza.Common.Models.ProductRequest GetProducts();
        IQueryable GetProducts();

        MaterialesIza.Common.Models.ProductRequest GetProductWithSalesById(int id);
    }
}
