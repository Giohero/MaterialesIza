﻿namespace MaterialesIza.Data
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
                    ("Doe", "Jhon", "jhon.doe@gmail.com", "987654321", "123456", "Admin");
                await CheckAdminAsync(user);

            }
            if (!this.dataContext.Employees.Any())
            {
                var user = await CheckUserAsync
                    ("Doe", "Jane", "jane.doe@gmail.com", "123456789", "123456", "Production");
                await CheckEmployeesAsync(user);

            }

            if (!this.dataContext.Clients.Any())
            {
                var user = await CheckUserAsync
                    ("Cena", "Jhon", "jhonCena.doe@gmail.com", "345523424", "123456", "Sales");
                await CheckClientsAsync(user);
            }

            if (!this.dataContext.Providers.Any())
            {
                var user = await CheckUserAsync
                    ("Cena", "Jhon", "jhonCena.doe@gmail.com", "345523424", "123456", "Sales");
                await CheckProvidersAsync(user);
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
            this.dataContext.Admins.Add(new Admin { user = user });
            await this.dataContext.SaveChangesAsync();
        }
        private async Task CheckEmployeesAsync(User user)
        {
            this.dataContext.Employees.Add(new Employee { user = user });
            await this.dataContext.SaveChangesAsync();
        }
        private async Task CheckClientsAsync(User user)
        {
            this.dataContext.Clients.Add(new Client { user = user });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProvidersAsync(User user)
        {
            this.dataContext.Providers.Add(new Provider { user = user });
            await this.dataContext.SaveChangesAsync();
        }


    }
}






