using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchasesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Purchase> purchases;
        public ObservableCollection<Purchase> Purchases
        {

            get { return this.purchases; }
            set { this.SetValue(ref this.purchases, value); }
        }

        public PurchasesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Purchase>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/Purchases");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myPurchases = (List<Purchase>)response.Result;
            this.Purchases = new ObservableCollection<Purchase>(myPurchases);
        }
    }
}
