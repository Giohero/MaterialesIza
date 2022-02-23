

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEmployeeRepository : IGenericRepository<MaterialesIza.Data.Entities.Employee>
    {
        IEnumerable<SelectListItem> GetComboEmployee();

        IQueryable GetEmployees();

        //MaterialesIza.Common.Models.EmployeeRequest GetEmployeeWithSalesByEmail(EmailRequest emailEmployee);
    }
}
