

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IServiceRepository : IGenericRepository<MaterialesIza.Data.Entities.Service>
    {
        IEnumerable<SelectListItem> GetComboService();
        IQueryable GetServices();
        IEnumerable<ServiceRequest> GetAllServicessWithType();
        //MaterialesIza.Common.Models.ServiceRequest GetServices();

    }
}
