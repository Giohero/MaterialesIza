namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    interface IClientRepository : IGenericRepository<Client>
    {
        IEnumerable<SelectListItem> GetComboClient();
    }
}
