

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class ServiceTypeRepository : GenericRepository<MaterialesIza.Data.Entities.ServiceType>, IServiceTypeRepository
    {
        private readonly DataContext dataContext;

        public ServiceTypeRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboServiceType()
        {
            var list = this.dataContext.ServiceTypes.Select(m => new SelectListItem
            {
                Text = m.TypeService,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un tipo de servicio)",
                Value = "0"
            });
            return list;

        }

        //public MaterialesIza.Common.Models.ServiceTypeRequest GetServiceTypes()
        //{
        //    var a = this.dataContext.ServiceTypes.FirstOrDefault();

        //    if (a == null)
        //    {
        //        return null;
        //    }

        //    var x = new ServiceTypeRequest
        //    {
        //        Id = a.Id,
        //        TypeService = a.TypeService
        //    };
        //    return x;
        //}
        public IQueryable GetServiceTypes()
        {
            return this.dataContext.ServiceTypes;
        }
    }
}
