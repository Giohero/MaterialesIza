using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddServicesViewModel:BaseViewModel
    {
        private readonly ApiService apiService;
        public string Name { get; set; }

        public string Description { get; set; }

        public string ServiceType { get; set; }

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
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Servicio", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una Descripcion", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(ServiceType))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Servicio", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var service = new ServiceRequest { Name = Name, Description = Description, ServiceType = ServiceType };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Services",
                service,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newService = (ServiceRequest)response.Result;
            MainViewModel.GetInstance().Services.Services.Add(newService);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddServicesViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
