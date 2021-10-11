namespace MaterialesIza.Data
{
    using MaterialesIza.Data.Entities;
    using MaterialesIza.Helpers;
    using Microsoft.AspNetCore.Identity;
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
                    ("Doe", "Jane", "jane.doe@gmail.com", "123456789", "123456", "Employee");
                //var sale = this.dataContext.Sales.FirstOrDefault();
                //var order = this.dataContext.Orders.FirstOrDefault();
                await CheckEmployeesAsync(user);



            }

            if (!this.dataContext.Clients.Any())
            {
                var user = await CheckUserAsync
                    ("Cena", "Jhon", "jhoncena@gmail.com", "345523424", "123456", "Client");
                await CheckClientsAsync(user);
            }

            if (!this.dataContext.Providers.Any())
            {
                var user = await CheckUserAsync
                    ("Travolta", "Jhon", "travolta@gmail.com", "345523424", "123456", "Provider");
                await CheckProvidersAsync(user);
            }

            if (!this.dataContext.ProductTypes.Any())
            {
               
                await this.CheckProductTypeAsync("Construccion");
            }

            if (!this.dataContext.Products.Any())
            {
                var productType = this.dataContext.ProductTypes.FirstOrDefault(p => p.Name == "Cobre");
                await this.CheckProductAsync("Cemento", 200, 100, "Bulto", productType);
                await this.CheckProductAsync("Cal", 20, 100, "Bulto", productType);
                await this.CheckProductAsync("Ladrillo", 5, 100, "Pieza", productType);
            }

            if (!this.dataContext.SaleDetails.Any())
            {
                var sale = this.dataContext.Sales.FirstOrDefault();
                var product = this.dataContext.Products.FirstOrDefault();
                await this.CheckSaleDetailAsync(sale,product);
            }

            if (!this.dataContext.Orders.Any())
            {
                var employee = this.dataContext.Employees.FirstOrDefault();
                var client = this.dataContext.Clients.FirstOrDefault();
                await this.CheckOrderAsync(employee,client);
            }

            if (!this.dataContext.OrderDetails.Any())
            {
                var order = this.dataContext.Orders.FirstOrDefault();
                var service = this.dataContext.Services.FirstOrDefault();
                await this.CheckOrderDetailAsync(order,service);
            }

            if (!this.dataContext.ServiceTypes.Any())
            {
                await this.CheckServiceTypeAsync("Mano Chango");
            }

            if (!this.dataContext.Services.Any())
            {
                var serviceType = this.dataContext.ServiceTypes.FirstOrDefault();
                await this.CheckServiceAsync("Mano Chango","Retroexcavadora",serviceType);

            }
            
            if (!this.dataContext.Sales.Any())
            {
                var client = this.dataContext.Clients.FirstOrDefault();
                var employee = this.dataContext.Employees.FirstOrDefault();
                await this.CheckSaleAsync(client,employee);
            }          

            if (!this.dataContext.Purchases.Any())
            {
                var provider = this.dataContext.Providers.FirstOrDefault();
                await this.CheckPurchaseAsync(provider);
            }

            if (!this.dataContext.PurchaseDetails.Any())
            {
                var product = this.dataContext.Products.FirstOrDefault();
                var purchase = this.dataContext.Purchases.FirstOrDefault();
                await this.CheckPurchaseDetailAsync(product,purchase);
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

        private async Task CheckOrderAsync(Employee employee, Client client)
        {
            this.dataContext.Orders.Add(new Order { Employee = employee, Client = client });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckOrderDetailAsync(Order order, Service service)
        {
            this.dataContext.OrderDetails.Add(new OrderDetail { Order = order, Service = service });
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

        private async Task CheckSaleAsync(Client client, Employee employee )
        {
            this.dataContext.Sales.Add(new Sale { Client = client, Employee = employee });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSaleDetailAsync(Sale sale, Product product)
        {
            this.dataContext.SaleDetails.Add(new SaleDetail { Sale = sale, Product = product });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProductTypeAsync(string name)
        {
            this.dataContext.ProductTypes.Add(new ProductType { Name = name });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProductAsync(string name, int price, int quantity, string description, ProductType productType)
        {
            this.dataContext.Products.Add(new Product { Name = name, Price = price, Quantity= quantity, Description = description, ProductTypes = productType });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPurchaseDetailAsync(Product product, Purchase purchase)
        {
            this.dataContext.PurchaseDetails.Add(new PurchaseDetail { Product = product, Purchase = purchase });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPurchaseAsync(Provider provider)
        {
            this.dataContext.Purchases.Add(new Purchase { Provider = provider });
            await this.dataContext.SaveChangesAsync();
        }
    }
}






