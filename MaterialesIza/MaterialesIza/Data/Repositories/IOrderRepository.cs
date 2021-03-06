using MaterialesIza.Common.Models;
using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
   public  interface IOrderRepository: IGenericRepository<MaterialesIza.Data.Entities.Order>
    {
        IQueryable GetOrder();

        IEnumerable<SelectListItem> GetComboOrder();


        IEnumerable<OrderRequest> GetOrders();
    }
}
