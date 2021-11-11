namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IClientRepository : IGenericRepository<Client>
    {
        IEnumerable<SelectListItem> GetComboClient();

        IQueryable GetClients();
    }
}
