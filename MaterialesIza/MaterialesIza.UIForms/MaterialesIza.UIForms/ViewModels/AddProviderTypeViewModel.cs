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
    public class AddProviderTypeViewModel : BaseViewModel
    {
        private readonly ApiService apiService;

        public string FirstName { get; set; }

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
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir el nombre del Proveedor", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var providerType = new ProviderTypeRequest { FirstName = FirstName };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/ProviderTypes",
                providerType,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newProviderType = (ProviderTypeRequest)response.Result;
            MainViewModel.GetInstance().ProviderTypes.AddProviderTypeToList(newProviderType);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddProviderTypeViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
