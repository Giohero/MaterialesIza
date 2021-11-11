

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<SelectListItem> GetComboEmployee();

        IQueryable GetEmployees();
    }
}
