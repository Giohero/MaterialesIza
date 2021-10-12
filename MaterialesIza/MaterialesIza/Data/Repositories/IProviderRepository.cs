using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    interface IProviderRepository : IGenericRepository<Provider>
    {
        IEnumerable<SelectListItem> GetComboProvider();
    }
}
