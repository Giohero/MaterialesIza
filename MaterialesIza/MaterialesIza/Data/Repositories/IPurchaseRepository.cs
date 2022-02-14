using MaterialesIza.Common.Models;
using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public interface IPurchaseRepository: IGenericRepository<MaterialesIza.Data.Entities.Purchase>
    {
        IEnumerable<SelectListItem> GetComboPurchase();

        MaterialesIza.Common.Models.ProviderRequest GetPurchases(EmailRequest emailRequest);
    }
}
