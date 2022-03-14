using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Services;
using System.Windows.Input;
using Xamarin.Forms;
using MaterialesIza.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddPurchaseViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public int Id { get; set; }

        public string PurchaseName { get; set; }

        public DateTime Date_purchase { get; set; }

        public double Total_purchase { get; set; }

        public double Iva_purchase { get; set; }

        public string Purchase_Remarks { get; set; }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        private IList<string> purchaseTypeList;
        public IList<string> PurchaseTypeList
        {
            get { return this.purchaseTypeList; }
            set { this.SetValue(ref this.purchaseTypeList, value); }
        }
        private async void LoadPurchaseTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseTypeRequest>(//¿¿¿PurchasesTypeRequest???
                url,
                "/api",
                "/PurchasesTypes",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            PurchaseTypeList = ((List<PurchaseTypeRequest>)response.Result).Select(pu => pu.PurchaseName).ToList();//no es posible convertir de int a string

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
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa el nombre de tu compra", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Date_purchase.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa la fecha de la compra", "Aceptar");
                return;
            }
            //if (string.IsNullOrEmpty(Total_purchase))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Ingresa el monto total de tu compra", "Aceptar");
            //    return;
            //}

            //if (string.IsNullOrEmpty(Iva_purchase))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Ingresa el monto de IVA de tu compra", "Aceptar");
            //    return;
            //}

            if (string.IsNullOrEmpty(Purchase_Remarks))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Compra", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var product = new PurchaseRequest { Date_purchase = Date_purchase, Total_purchase = Total_purchase, Iva_purchase = Iva_purchase, Purchase_Remarks = Purchase_Remarks };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Purchases",
                product,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newPurchase = (PurchaseRequest)response.Result;
            MainViewModel.GetInstance().Purchases.AddPurchaseToList(newPurchase);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddPurchaseViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
            this.LoadPurchaseTypes();
        }
    }
}
