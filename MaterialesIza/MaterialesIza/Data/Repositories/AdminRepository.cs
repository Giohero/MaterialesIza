using MaterialesIza.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Repositories
{
    public class AdminRepository:GenericRepository<Admin>,IAdminRepository
    {
        private readonly DataContext dataContext;

        public AdminRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetAdmins()
        {
            return this.dataContext.Admins
                .Include(p => p.User.FullName);
        }
        public IEnumerable<SelectListItem> GetComboAdmin()
        {
            var list = this.dataContext.Admins.Select(m => new SelectListItem
            {
                Text = m.User.FullName,
                Value = $"{m.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un Admin)",
                Value = "0"
            });
            return list;
        }
    }
}
