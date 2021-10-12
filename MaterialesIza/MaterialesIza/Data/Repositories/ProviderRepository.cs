
namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        private readonly DataContext dataContext;

        public ProviderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
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
                Text = "(Selecciona un cliente)",
                Value = "0"
            });
            return list;
        }

    }
}
