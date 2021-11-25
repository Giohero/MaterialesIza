
using MaterialesIza.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MaterialesIza.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }

        public ProductsViewModel Products { get; set; }

        public ProductTypesViewModel ProductTypes { get; set; }

        public ServicesViewModel Services { get; set; }

        public ServiceTypesViewModel ServiceTypes { get; set; }

        public AdminsViewModel Admins { get; set; }

        public ClientsViewModel Clients { get; set; }

        public EmployeesViewModel Employees { get; set; }

        public OrdersViewModel Orders { get; set; }

        public OrderDetailsViewModel OrderDetails { get; set; }

        public ProvidersViewModel Providers { get; set; }

        public PurchasesViewModel Purchases { get; set; }

        public PurchaseDetailsViewModel PurchaseDetails { get; set; }

        public SalesViewModel Sales { get; set; }

        public SaleDetailsViewModel SaleDetails { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MainViewModel()
        {
            instance = this;
            //this.Login = new LoginViewModel();
            LoadMenu();
        }

        private void LoadMenu()
        {
            var menus = new List<Menu>
            {
                new Menu
            {
                Icon="ic_assignment",
                PageName="ProductsPage",
                Title="Products"
            },
                new Menu
            {
                Icon="Shipping",
                PageName="ServicePage",
                Title="Services"
            },
                   new Menu
            {
                Icon="Lines",
                PageName="ServiceTypesPage",
                Title="ServiceType"
            },
                      new Menu
            {
                Icon="LineP",
                PageName="ProductsTypePage",
                Title="ProductsType"
            },

                new Menu
            {
                Icon="",
                PageName="AdminsPage",
                Title="Admins"

            },
                 new Menu
            {
                Icon="",
                PageName="ClientsPage",
                Title="Clients"

            },
                new Menu
            {
                Icon="",
                PageName="EmployeesPage",
                Title="Employees"

            },
                new Menu
            {
                Icon="",
                PageName="ProvidersPage",
                Title="Providers"

            },
                new Menu
                 {
                Icon="",
                PageName="PurchasesPage",
                Title="Purchases"

            },
                new Menu
                  {
                Icon="",
                PageName="PurchaseDetailsPage",
                Title="PurchaseDetails"

            },
                new Menu
                   {
                Icon="",
                PageName="OrdersPage",
                Title="Orders"

            },

                new Menu
                   {
                Icon="",
                PageName="OrderDetailsPage",
                Title="OrderDetails"

            },

                new Menu
                   {
                Icon="",
                PageName="SalesPage",
                Title="Sales"

            },
                new Menu
                   {
                Icon="",
                PageName="SaleDetailsPage",
                Title="SaleDetails"

            },
                new Menu
            {
                Icon="Setup",
                PageName="SetupPage",
                Title="Setup"
            },
                new Menu
            {
                Icon="Info",
                PageName="AboutPage",
                Title="About"
            },
                new Menu
            {
                Icon="exit",
                PageName="LoginPage",
                Title="Close session"

            },
                      


            };
            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m=>new MenuItemViewModel
            {
                Icon= m.Icon,
                PageName =m.PageName,
                Title = m.Title
            }).ToList());
        }

        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
