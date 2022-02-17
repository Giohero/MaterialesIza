using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface IProviderRepository : IGenericRepository<MaterialesIza.Data.Entities.Provider>
    {
        IEnumerable<SelectListItem> GetComboProvider();

        IQueryable GetProviders();

        MaterialesIza.Common.Models.ProviderRequest GetPurchasesByEmailProvider(EmailRequest emailRequest);
    }
}
