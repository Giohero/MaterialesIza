

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IServiceRepository : IGenericRepository<MaterialesIza.Data.Entities.Service>
    {
        IEnumerable<SelectListItem> GetComboService();
        IQueryable GetServices();
        //MaterialesIza.Common.Models.ServiceRequest GetServices();

    }
}
