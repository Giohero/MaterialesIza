
namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        private readonly DataContext dataContext;

        public ProviderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetProviders()
        {
            return this.dataContext.Providers
                .Include(p => p.User);
        }
        public IEnumerable<SelectListItem> GetComboProvider()
        {
            var list = this.dataContext.Providers.Select(m => new SelectListItem
            {
                Text = m.User.FullName,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un Proveedor)",
                Value = "0"
            });
            return list;
        }

    }
}
