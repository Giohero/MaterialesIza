using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
   public  interface IOrderRepository: IGenericRepository<Order>
    {
        IEnumerable<SelectListItem> GetComboOrder();

        IQueryable GetOrders();
    }
}
