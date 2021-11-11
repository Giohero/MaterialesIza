using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MaterialesIza.Data.Repositories
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        IEnumerable<SelectListItem> GetComboSale();

        IQueryable GetSale();
    }
}
