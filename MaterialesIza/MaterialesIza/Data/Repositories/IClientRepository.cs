namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IClientRepository : IGenericRepository<MaterialesIza.Data.Entities.Client>
    {
        IEnumerable<SelectListItem> GetComboClient();

        IQueryable GetClientsWithUser();

        IEnumerable<ClientRequest> GetClients();

        MaterialesIza.Common.Models.ClientRequest GetClientWithOrdersByEmail(EmailRequest emailClient);
    }
}
