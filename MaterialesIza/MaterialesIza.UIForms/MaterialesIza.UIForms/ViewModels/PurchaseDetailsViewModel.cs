using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchaseDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<PurchaseDetailsRequest> purchaseDetails;
        public ObservableCollection<PurchaseDetailsRequest> PurchaseDetails
        {

            get { return this.purchaseDetails; }
            set { this.SetValue(ref this.purchaseDetails, value); }
        }
        //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadProducts();
        }

        public PurchaseDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseDetailsRequest>(
              url,
              "/api",
              "/PurchaseDetails",
              "bearer",
              MainViewModel.GetInstance().Token.Token);
            //Final de carga
            this.IsRefreshing = false;
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myPurchaseDetails = (List<PurchaseDetailsRequest>)response.Result;
            this.PurchaseDetails = new ObservableCollection<PurchaseDetailsRequest>(myPurchaseDetails);
        }
    }
}
