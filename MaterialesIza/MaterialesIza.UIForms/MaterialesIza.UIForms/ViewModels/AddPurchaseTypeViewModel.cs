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
    public class AddPurchaseTypeViewModel : BaseViewModel
    {
        private readonly ApiService apiService;

        public string PurchaseName { get; set; }

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
            if (string.IsNullOrEmpty(PurchaseName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una Compra", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var productType = new PurchaseTypeRequest { PurchaseName = PurchaseName };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/PurchaseTypes",
                productType,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newPurchaseType = (PurchaseTypeRequest)response.Result;
            MainViewModel.GetInstance().PurchaseTypes.AddPurchaseTypeToList(newPurchaseType);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddPurchaseTypeViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
