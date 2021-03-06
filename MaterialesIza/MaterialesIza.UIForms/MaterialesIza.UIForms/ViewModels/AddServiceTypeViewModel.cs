using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddServiceTypeViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public string TypeService { get; set; }

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
            if (string.IsNullOrEmpty(TypeService))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Servicio", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var serviceType = new ServiceTypeRequest { TypeService = TypeService };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/ServiceTypes",
                serviceType,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newServiceType = (ServiceTypeRequest)response.Result;
            MainViewModel.GetInstance().ServiceTypes.AddServiceTypeToList(newServiceType);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddServiceTypeViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
