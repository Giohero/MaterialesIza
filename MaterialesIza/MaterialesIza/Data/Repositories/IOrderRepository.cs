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
        IEnumerable<SelectListItem> GetComboOrder();

        MaterialesIza.Common.Models.OrderRequest GetOrders();



    }
}
