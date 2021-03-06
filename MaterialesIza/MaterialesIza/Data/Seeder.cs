namespace MaterialesIza.Data
{
    using MaterialesIza.Data.Entities;
    using MaterialesIza.Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Seeder
    {

        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;


        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Employee");
            await this.userHelper.CheckRoleAsync("Client");
            await this.userHelper.CheckRoleAsync("Provider");


            if (!this.dataContext.Admins.Any())
            {
                var user = await CheckUserAsync
                    ("Zarate", "Yair", "yairz.@gmail.com", "454889", "123456", "Admin");
                await CheckAdminAsync(user);
            }

            if (!this.dataContext.Employees.Any())
            {
                var user = await CheckUserAsync
                    ("Saldaña", "Jaime", "jaime.Sal@gmail.com", "123456789", "123456", "Employee");
                await CheckEmployeesAsync(user);

                user = await CheckUserAsync
                    ("Sandoval", "Jesus", "jesus.Sal@gmail.com", "123456789", "123456", "Employee");
                await CheckEmployeesAsync(user);

                user = await CheckUserAsync
                    ("Baldomero", "Santiago", "santiago.Sal@gmail.com", "123456789", "123456", "Employee");
                await CheckEmployeesAsync(user);
            }

            if (!this.dataContext.Clients.Any())
            {
                var user = await CheckUserAsync
                    ("Hernández", "Giovanni", "firmalagio@gmail.com", "345523424", "123456", "Client");
                await CheckClientsAsync(user);
                user = await CheckUserAsync
                    ("Hernández", "Javier", "firmalajavi@gmail.com", "3333333", "123456", "Client");
                await CheckClientsAsync(user);
                user = await CheckUserAsync
                    ("Hernández", "Antonio", "firmalaanto@gmail.com", "3333333", "123456", "Client");
                await CheckClientsAsync(user);


            }

            if (!this.dataContext.Providers.Any())
            {
                var user = await CheckUserAsync
                    ("Cruz", "Alexis", "alexiscz@gmail.com", "345523424", "123456", "Provider");
                await this.CheckProvidersAsync(user);

                user = await CheckUserAsync
                    ("Valdez", "Cristobal", "cristo21@gmail.com", "123456789", "654321", "Provider");
                await this.CheckProvidersAsync(user);
            }


            if (!this.dataContext.ProductTypes.Any())
            {
                await this.CheckProductTypeAsync("Construccion");
                await this.CheckProductTypeAsync("Metales");
            }

            if (!this.dataContext.Sales.Any())
            {

                var client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Giovanni");

                var employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckSaleAsync(DateTime.Now, 234, 16, "Venta 1", client, employee);

                client = this.dataContext.Clients
                   .Include(c => c.User)
                   .FirstOrDefault(c => c.User.FirstName == "Javier");

                employee = this.dataContext.Employees
                   .Include(c => c.User)
                   .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckSaleAsync(DateTime.Now, 5345, 16, "Venta 2", client, employee);

                client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Antonio");

                employee = this.dataContext.Employees
                   .Include(c => c.User)
                   .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckSaleAsync(DateTime.Now, 45698, 16, "Venta 3", client, employee);
            }

            if (!this.dataContext.Products.Any())
            {
                var productType = this.dataContext.ProductTypes.FirstOrDefault();
                await this.CheckProductAsync("Cemento", 200, "Bulto", productType);

                productType = this.dataContext.ProductTypes.FirstOrDefault(p => p.Name == "Construccion");
                await this.CheckProductAsync("Cal", 20, "Bulto", productType);

                productType = this.dataContext.ProductTypes.FirstOrDefault(p => p.Name == "Construccion");
                await this.CheckProductAsync("Ladrillo", 5, "Pieza", productType);

                productType = this.dataContext.ProductTypes.FirstOrDefault(p => p.Name == "Metales");
                await this.CheckProductAsync("Varilla", 10, "Pieza", productType);

            }

            if (!this.dataContext.SaleDetails.Any())
            {
                var product = this.dataContext.Products.FirstOrDefault();
                var sale = this.dataContext.Sales.FirstOrDefault(c => c.Client.User.FirstName == "Javier");
                await this.CheckSaleDetailAsync(4, product, sale);

                product = this.dataContext.Products.FirstOrDefault();
                sale = this.dataContext.Sales.FirstOrDefault();
                await this.CheckSaleDetailAsync(10, product, sale);

                product = this.dataContext.Products.FirstOrDefault(c => c.Name == "Cal");
                sale = this.dataContext.Sales.FirstOrDefault();
                await this.CheckSaleDetailAsync(45, product, sale);

                product = this.dataContext.Products.FirstOrDefault(c => c.Name == "Cal");
                sale = this.dataContext.Sales.FirstOrDefault(c => c.Client.User.FirstName == "Antonio");
                await this.CheckSaleDetailAsync(80, product, sale);
            }


            if (!this.dataContext.ServiceTypes.Any())
            {
                await this.CheckServiceTypeAsync("Flete");
                await this.CheckServiceTypeAsync("Mano Chango");
                await this.CheckServiceTypeAsync("Retroexcavadora");
            }

            if (!this.dataContext.Orders.Any())
            {
                var client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Giovanni");

                var employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckOrderAsync(DateTime.Now, 234, 16, "Prueba Order 1", employee,client);

                client = this.dataContext.Clients
                   .Include(c => c.User)
                   .FirstOrDefault(c => c.User.FirstName == "Yair");
                employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckOrderAsync(DateTime.Now, 5680, 16, "Prueba Order 4", employee, client);
                client = this.dataContext.Clients
                   .Include(c => c.User)
                   .FirstOrDefault(c => c.User.FirstName == "Antonio");
                employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jaime");
                await this.CheckOrderAsync(DateTime.Now, 8680, 16, "Prueba Order 5", employee, client);

                client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Javier");
                employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jesus");
                await this.CheckOrderAsync(DateTime.Now, 698, 16, "Prueba Order 2", employee, client);
                client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Antonio");
                employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jesus");
                await this.CheckOrderAsync(DateTime.Now, 6998, 16, "Prueba Order 6", employee, client);

                client = this.dataContext.Clients
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Yair");
                employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Santiago");
                await this.CheckOrderAsync(DateTime.Now, 6498, 16, "Prueba Order 3", employee, client);



            }

            if (!this.dataContext.Services.Any())
            {
                var serviceType = this.dataContext.ServiceTypes.FirstOrDefault();
                await this.CheckServiceAsync("Maquinaria", "Excavadora", serviceType);

                serviceType = this.dataContext.ServiceTypes.FirstOrDefault(s =>s.TypeService == "Retroexcavadora");
                await this.CheckServiceAsync("Viaje", "Trascavo", serviceType);
            }

            if (!this.dataContext.OrderDetails.Any())
            {
                
                var service = this.dataContext.Services.FirstOrDefault();
                var order = this.dataContext.Orders.FirstOrDefault();
                await this.CheckOrderDetailAsync( 2, 500, service,order);

                service = this.dataContext.Services.FirstOrDefault();
                order = this.dataContext.Orders.FirstOrDefault(p => p.Id == 2);
                await this.CheckOrderDetailAsync(1, 300, service, order);

                service = this.dataContext.Services.FirstOrDefault();
                order = this.dataContext.Orders.FirstOrDefault(p => p.Id == 3);
                await this.CheckOrderDetailAsync(3, 800, service, order);

                service = this.dataContext.Services.FirstOrDefault();
                order = this.dataContext.Orders.FirstOrDefault(p => p.Id == 4);
                await this.CheckOrderDetailAsync(4, 600, service, order);

                 service = this.dataContext.Services.FirstOrDefault();
                 order = this.dataContext.Orders.FirstOrDefault(p => p.Id == 5);
                await this.CheckOrderDetailAsync(5, 1000, service, order);

                service = this.dataContext.Services.FirstOrDefault();
                order = this.dataContext.Orders.FirstOrDefault(p => p.Id == 6);
                await this.CheckOrderDetailAsync(4, 700, service, order);
            }

            if (!this.dataContext.Purchases.Any())
            {
                var Provider = this.dataContext.Providers
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Alexis");

                var Employee = this.dataContext.Employees
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.User.FirstName == "Jaime");

                await this.CheckPurchaseAsync(DateTime.Now, 220, 10, "Compra 1", Provider,Employee);

            }

            if (!this.dataContext.PurchaseDetails.Any())
            {
                //var Provider = this.dataContext.Providers
                //    .Include(c => c.User)
                //    .FirstOrDefault(c => c.User.FirstName == "Cristobal");
                //await CheckPurchaseAsync(Provider);

                //var product = this.dataContext.Products.FirstOrDefault();
                var purchase = this.dataContext.Purchases.FirstOrDefault();
                var product = this.dataContext.Products.FirstOrDefault();
                await this.CheckPurchaseDetailAsync(100, product, purchase);
            }
        }

        private async Task<User> CheckUserAsync(string lastName, string firstName, string mail, string phone, string password, string rol)

        {

            var user = await userHelper.GetUserByEmailAsync(mail);

            if (user == null)

            {

                user = new User

                {

                    FirstName = firstName,

                    LastName = lastName,

                    Email = mail,

                    UserName = mail,

                    PhoneNumber = phone,

                };

                var result = await userHelper.AddUserAsync(user, password);

                if (result != IdentityResult.Success)

                {

                    throw new InvalidOperationException("No se puede crear el usuario en la base de datos");

                }
                await userHelper.AddUserToRoleAsync(user, rol);

            }

            return user;

        }

        private async Task CheckAdminAsync(User user)
        {
            this.dataContext.Admins.Add(new Admin { User = user });
            await this.dataContext.SaveChangesAsync();
        }
        private async Task CheckEmployeesAsync(User user)
        {
            this.dataContext.Employees.Add(new Employee { User = user });
            await this.dataContext.SaveChangesAsync();
        }
        private async Task CheckClientsAsync(User user)
        {
            this.dataContext.Clients.Add(new Client { User = user });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProvidersAsync(User user)
        {
            this.dataContext.Providers.Add(new Provider { User = user });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckOrderAsync(DateTime date_order, double total_order, double iva_order, string order_remarks, Employee employee, Client client)
        {
            this.dataContext.Orders.Add(new Order { Date_Order = date_order, Total_Order = total_order, Iva_Order = iva_order, Order_Remarks = order_remarks, Employee = employee, Client = client });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckOrderDetailAsync(int quantity, double price,   Service service,Order order)
        {
            this.dataContext.OrderDetails.Add(new OrderDetail { Quantity = quantity, Price = price,   Service = service,Order = order });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckServiceTypeAsync(string servicetype)
        {
            this.dataContext.ServiceTypes.Add(new ServiceType { TypeService = servicetype });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckServiceAsync(string name, string description, ServiceType serviceType)
        {
            this.dataContext.Services.Add(new Service { Name = name, Description = description, ServiceType = serviceType });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSaleAsync(DateTime date_sale, double total_sale, double iva_sale, string sales_remarks, Client client, Employee employee )
        {
            this.dataContext.Sales.Add(new Sale { Date_Sale = date_sale, Total_Sale = total_sale, Iva_Sale = iva_sale, Sales_Remarks = sales_remarks, Client = client, Employee = employee });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSaleDetailAsync(int quantity,  Product product, Sale sale)
        {
            this.dataContext.SaleDetails.Add(new SaleDetail { Quantity = quantity,  Product = product, Sale = sale });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProductTypeAsync(string name)
        {
            this.dataContext.ProductTypes.Add(new ProductType { Name = name });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProductAsync(string name, int price, string description, ProductType productType)
        {
            this.dataContext.Products.Add(new Product { Name = name, Price = price,  Description = description, ProductTypes = productType });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPurchaseDetailAsync( int quantity, Product product, Purchase purchase )
        {
            this.dataContext.PurchaseDetails.Add(new PurchaseDetail { Quantity = quantity,   Product = product, Purchase = purchase });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPurchaseAsync(DateTime date_purchase, double total_purchase, double iva_purchase, string purchase_remarks, Provider provider, Employee employee)
        {
            this.dataContext.Purchases.Add(new Purchase { Date_purchase = date_purchase, Total_purchase = total_purchase, Iva_purchase = iva_purchase, Purchase_Remarks = purchase_remarks, Provider = provider , Employee = employee});
            await this.dataContext.SaveChangesAsync();
        }
    }
}






