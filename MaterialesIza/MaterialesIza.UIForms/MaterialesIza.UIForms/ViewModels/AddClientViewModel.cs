using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using MaterialesIza.Common.Services;
using MaterialesIza.Common.Models;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddClientViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        //public ICollection<OrderRequest> Orders { get; set; }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un nombre", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un apellido", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un email", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un numero de telefono", "Aceptar");
                return;
            }
            //if (Orders == null)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una orden", "Aceptar");
            //    return;
            //}

            isEnabled = false;
            isRunning = true;
            var client = new ClientRequest { FirstName = FirstName, LastName = LastName, Email = Email, PhoneNumber = PhoneNumber, /*Orders = Orders*/ };
            var url = Application.Current.Resources["URLApi"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Clients",
                client,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newClient = (ClientRequest)response.Result;
            MainViewModel.GetInstance().Clients.Clients.Add(newClient);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }
        public AddClientViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
