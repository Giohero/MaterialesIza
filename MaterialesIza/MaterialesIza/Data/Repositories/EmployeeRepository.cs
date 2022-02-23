

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
    using MaterialesIza.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeRepository : GenericRepository<MaterialesIza.Data.Entities.Employee>, IEmployeeRepository
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

        //public MaterialesIza.Common.Models.EmployeeRequest GetEmployeeWithOrdersByEmail(EmailRequest emailEmployee)
        //{
        //    var c = this.dataContext.Employees
        //        .Include(c => c.User)
        //        .Include(c => c.Sales)
        //        .ThenInclude(o => o.SaleDetails)
        //        .ThenInclude(od => od.Product)
        //        .ThenInclude(s => s.ProductTypes)
        //        .Include(c => c.Sales)
        //        .ThenInclude(c => c.Client)
        //        .ThenInclude(c => c.User)
        //        .FirstOrDefault(c => c.User.Email.ToLower() == emailEmployee.Email);
        //    if (c == null)
        //    {
        //        return null;
        //    }
        //    var x = new EmployeeRequest
        //    {
        //        Id = c.Id,
        //        FirstName = c.User.FirstName,
        //        LastName = c.User.LastName,
        //        Email = c.User.Email,
        //        PhoneNumber = c.User.PhoneNumber,
        //        Order = c.Order?.Select(o => new OrderRequest
        //        {
        //            Id = o.Id,
        //            OrderDetails = o.,
        //            Total_Sale = o.Total_Sale,
        //            Iva_Sale = o.Iva_Sale,
        //            Sales_Remarks = o.Sales_Remarks,
        //            Client = new ClientRequest
        //            {
        //                Id = o.Client.Id,
        //                Email = o.Client.User.Email,
        //                FirstName = o.Client.User.FirstName,
        //                LastName = o.Client.User.LastName,
        //                PhoneNumber = o.Client.User.PhoneNumber,
        //            },
        //            Order = o.SaleDetails?.Select(od => new SaleDetailsRequest
        //            {
        //                Id = od.Id,
        //                Quantity = od.Quantity,
        //                Product = new ProductRequest
        //                {

        //                    Id = od.Product.Id,
        //                    Name = od.Product.Name,
        //                    Price = od.Product.Price,
        //                    Description = od.Product.Description,
        //                    ProductTypes = od.Product.ProductTypes.Name

        //                }

        //            })/*.Where(od => od.Date_Sale != null)*/.ToList()
        //        }).ToList()
        //    };

        //    return x;
        //}







        //public MaterialesIza.Common.Models.EmployeeRequest GetEmployeeWithOrdersByEmail(EmailRequest emailEmployeeO)
        //{
        //}

    }
}
