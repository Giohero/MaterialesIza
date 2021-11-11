using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchaseDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<PurchaseDetail> purchaseDetails;
        public ObservableCollection<PurchaseDetail> PurchaseDetails
        {

            get { return this.purchaseDetails; }
            set { this.SetValue(ref this.purchaseDetails, value); }
        }

        public PurchaseDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<PurchaseDetail>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/PurchaseDetails");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myPurchaseDetails = (List<PurchaseDetail>)response.Result;
            this.PurchaseDetails = new ObservableCollection<PurchaseDetail>(myPurchaseDetails);
        }
    }
}
