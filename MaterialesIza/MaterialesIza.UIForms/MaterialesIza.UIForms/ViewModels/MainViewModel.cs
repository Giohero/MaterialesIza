
using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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



        public AddProductViewModel AddProduct { get; set; }
        public ICommand AddProductCommand { get { return new RelayCommand(GoProductCommand); } }

        private async void GoProductCommand()
        {
            this.AddProduct = new AddProductViewModel();
            await App.Navigator.PushAsync(new AddProductsPage());
        }

        public AddServicesViewModel AddServices { get; set; }
        public ICommand AddServiceCommand { get { return new RelayCommand(GoServiceCommand); } }

        private async void GoServiceCommand()
        {
            this.AddServices = new AddServicesViewModel();
            await App.Navigator.PushAsync(new AddServicesPage());
        }

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
                Icon="Admin",
                PageName="AdminsPage",
                Title="Admins"

                },
                 new Menu
                {
                Icon="client",
                PageName="ClientsPage",
                Title="Clients"

                },
                new Menu
                {
                Icon="employee",
                PageName="EmployeesPage",
                Title="Employees"

                },
                new Menu
                {
                Icon="provider",
                PageName="ProvidersPage",
                Title="Providers"

                },
                new Menu
                {
                Icon="purchase",
                PageName="PurchasesPage",
                Title="Purchases"

                },
                new Menu
                {
                Icon="PurchaseDetail",
                PageName="PurchaseDetailsPage",
                Title="PurchaseDetails"

                },
                new Menu
                   {
                Icon="order",
                PageName="OrdersPage",
                Title="Orders"
                },

                new Menu
                {
                Icon="OrderDetail",
                PageName="OrderDetailsPage",
                Title="OrderDetails"
                },

                new Menu
                {
                Icon="sale",
                PageName="SalesPage",
                Title="Sales"
                },
                new Menu
                {
                Icon="SalesDetail",
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
