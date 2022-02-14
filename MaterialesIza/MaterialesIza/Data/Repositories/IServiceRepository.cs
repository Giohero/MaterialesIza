

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface IServiceRepository : IGenericRepository<MaterialesIza.Data.Entities.Service>
    {
        IEnumerable<SelectListItem> GetComboService();

        MaterialesIza.Common.Models.ServiceRequest GetServices();

    }
}
