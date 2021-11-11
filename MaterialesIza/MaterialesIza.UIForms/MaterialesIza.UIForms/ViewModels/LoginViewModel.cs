﻿using GalaSoft.MvvmLight.Command;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        public LoginViewModel()
        {
            this.Email = "yairz.@gmail.com";
            this.Password = "123456";
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Email", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una contraseña", "Aceptar");
                return;
            }
            if (!this.Email.Equals("yairz.@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email o contraseña incorrecta", "Aceptar");
                return;
            }
            //await Application.Current.MainPage.DisplayAlert("OK", "LIIISTO", "Aceptar");
            //return;

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

            MainViewModel.GetInstance().ProductTypes = new ProductTypesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductTypesPage());

            MainViewModel.GetInstance().Services = new ServicesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ServicePage());

            MainViewModel.GetInstance().ServiceTypes = new ServiceTypesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceTypesPage());

            MainViewModel.GetInstance().Admins = new AdminsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AdminsPage());

            MainViewModel.GetInstance().Clients = new ClientsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ClientsPage());

            MainViewModel.GetInstance().Employees = new EmployeesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new EmployeesPage());

            MainViewModel.GetInstance().Orders = new OrdersViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new OrdersPage());

            MainViewModel.GetInstance().OrderDetails = new OrderDetailsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new OrderDetailsPage());

            MainViewModel.GetInstance().Providers = new ProvidersViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProvidersPage());

            MainViewModel.GetInstance().Purchases = new PurchasesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PurchasesPage());

            MainViewModel.GetInstance().PurchaseDetails = new PurchaseDetailsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PurchaseDetailsPage());

            MainViewModel.GetInstance().Sales = new SalesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new SalesPage());

            MainViewModel.GetInstance().SaleDetails = new SaleDetailsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new SaleDetailsPage());
        }
    }
}
