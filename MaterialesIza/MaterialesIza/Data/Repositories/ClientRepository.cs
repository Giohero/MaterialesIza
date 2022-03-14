

namespace MaterialesIza.Data.Repositories
{
    using MaterialesIza.Common.Models;
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
        public IQueryable GetClientsWithUser()
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

        public IEnumerable<ClientRequest> GetClients()
        {
            var i = this.dataContext.Clients
                .Include(i => i.User);

            if (i == null)
            {
                return null;
            }

            var x = i.Select(a => new ClientRequest
            {
                Id = a.Id,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber
            }).ToList();

            return x;

        }

        //TODO Modificar el modelo adecuadamente - Si
        //TODO metodo GetEmployeeWithOrdersByEmail - Si
        //TODO metodo GetOrders - Si
        //TODO METODO GetProductWithSalesById - 


        public MaterialesIza.Common.Models.ClientRequest GetClientWithOrdersByEmail(EmailRequest emailClient)
        {
            var c = this.dataContext.Clients
                .Include(c => c.User)
                //.Include(c => c.Orders)
                //.ThenInclude(o => o.OrderDetails)
                //.ThenInclude(od => od.Service)
                //.ThenInclude(s => s.ServiceType)
                //.Include(c => c.Orders)
                //.ThenInclude(c => c.Employee)
                //.ThenInclude(c => c.User)
                .FirstOrDefault(c => c.User.Email.ToLower() == emailClient.Email);
            if (c == null)
            {
                return null;
            }
            var x = new ClientRequest
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Email = c.User.Email,
                PhoneNumber = c.User.PhoneNumber,
                //Orders = c.Orders?.Select(o => new OrderRequest
                //{
                //    Id = o.Id,
                //    Date_Order = o.Date_Order,
                //    Total_Order = o.Total_Order,
                //    Iva_Order = o.Iva_Order,
                //    Order_Remarks = o.Order_Remarks,
                //    Employee = new EmployeeRequest
                //    {
                //        Id = o.Employee.Id,
                //        Email = o.Employee.User.Email,
                //        FirstName = o.Employee.User.FirstName,
                //        LastName = o.Employee.User.LastName,
                //        PhoneNumber = o.Employee.User.PhoneNumber,
                //    },
                //    OrderDetails = o.OrderDetails?.Select(od => new OrderDetailsRequest
                //    {
                //        Id = od.Id,
                //        Quantity = od.Quantity,
                //        Price = od.Price,
                //        Service = new ServiceRequest
                //        {

                //            Id = od.Service.Id,
                //            Name = od.Service.Name,
                //            Description = od.Service.Description,
                //            ServiceType = od.Service.ServiceType.TypeService


                //        }

                //    })/*.Where(od => od.Date_Order != null)*/.ToList()
                //}).ToList()
            };

            return x;
        }
    }
}
