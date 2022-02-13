

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext dataContext;

        public ClientRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable GetClients()
        {
            return this.dataContext.Clients
                .Include(c => c.User);
        }

        public IEnumerable<SelectListItem> GetComboClient()
        {
            var list = this.dataContext.Clients.Select(c => new SelectListItem
            {
                Text = c.User.FullName,
                Value = $"{c.Id}"
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
