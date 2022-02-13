using MaterialesIza.Common.Models;
using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MaterialesIza.Data.Repositories
{
    public interface ISaleRepository : IGenericRepository<MaterialesIza.Data.Entities.Sale>
    {
        IEnumerable<SelectListItem> GetComboSale();

        MaterialesIza.Common.Models.EmployeeRequest GetSale(EmailRequest emailClient);
    }
}
