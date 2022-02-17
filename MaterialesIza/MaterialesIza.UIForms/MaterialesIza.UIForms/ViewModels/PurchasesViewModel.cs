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
    public class PurchasesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<PurchaseRequest> purchases;
        public ObservableCollection<PurchaseRequest> Purchases
        {

            get { return this.purchases; }
            set { this.SetValue(ref this.purchases, value); }
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
        public PurchasesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseRequest>(
              url,
              "/api",
              "/Purchases",
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
            var myPurchases = (List<PurchaseRequest>)response.Result;
            this.Purchases = new ObservableCollection<PurchaseRequest>(myPurchases);
        }
    }
}
