

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ServiceRepository : GenericRepository<MaterialesIza.Data.Entities.Service>, IServiceRepository
    {
        private readonly DataContext dataContext;

        public ServiceRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboService()
        {
            var list = this.dataContext.Services.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un servicio)",
                Value = "0"
            });
            return list;
        }

        public MaterialesIza.Common.Models.ServiceRequest GetServices()
        {
            var a = this.dataContext.Services
                .Include(od => od.OrderDetails)
                .ThenInclude(s => s.Service)
                .ThenInclude(st => st.ServiceType)
                .FirstOrDefault();

            if (a == null)
            {
                return null;
            }

            var x = new ServiceRequest
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ServiceType = a.ServiceType.TypeService

            };
            return x;

        }
    }
}
