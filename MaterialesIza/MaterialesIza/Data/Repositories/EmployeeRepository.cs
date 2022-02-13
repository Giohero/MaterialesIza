

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext dataContext;

        public EmployeeRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetEmployees()
        {
            return this.dataContext.Employees
                .Include(e => e.User);
        }
        public IEnumerable<SelectListItem> GetComboEmployee()
        {
            var list = this.dataContext.Employees.Select(e => new SelectListItem
            {
                Text = e.User.FullName,
                Value = $"{e.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un empleado)",
                Value = "0"
            });
            return list;
        }

    }
}
