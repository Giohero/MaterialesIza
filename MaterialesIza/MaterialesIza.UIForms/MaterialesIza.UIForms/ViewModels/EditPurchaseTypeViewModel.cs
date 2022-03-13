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
    public class EditPurchaseTypeViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public PurchaseTypeRequest PurchasesTypes { get; set; }

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
        public ICommand DeleteCommand { get { return new RelayCommand(Delete); } }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Seguro que deseas borrar?", "Si", "No");
            if (!confirm)
                return;

            isEnabled = false;
            isRunning = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(url,
                "/api",
                "/PurchasesTypes",
                PurchasesTypes.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().PurchaseTypes.DeletePurchaseTypeInList(PurchasesTypes.Id);
            this.isEnabled = true;
            this.isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(PurchasesTypes.PurchaseName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un tipo de compra", "Aceptar");
                return;
            }
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/PurchaseTypes",
                PurchasesTypes.Id,
                PurchasesTypes,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyPurchaseType = (PurchaseTypeRequest)response.Result;
            MainViewModel.GetInstance().PurchaseTypes.UpdatePurchaseTypeToList(modifyPurchaseType);
            this.isEnabled = true;
            this.isRunning = false;
            await App.Navigator.PopAsync();
        }
        public EditPurchaseTypeViewModel(PurchaseTypeRequest purchaseType)
        {
            this.PurchasesTypes = purchaseType;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }
    }
}

