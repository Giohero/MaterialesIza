using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public interface IPurchaseDetailRepository : IGenericRepository<PurchaseDetail>
    {
        IEnumerable<SelectListItem> GetComboPurchaseDetails();

        IQueryable GetPurchaseDetails();
    }
}
