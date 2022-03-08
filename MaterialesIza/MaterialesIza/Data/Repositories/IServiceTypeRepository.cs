

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IServiceTypeRepository : IGenericRepository<MaterialesIza.Data.Entities.ServiceType>
    {
        IEnumerable<SelectListItem> GetComboServiceType();

        //MaterialesIza.Common.Models.ServiceTypeRequest GetServiceTypes();
        IQueryable GetServiceTypes();
    }
}
