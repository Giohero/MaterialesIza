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
    public class SaleDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<SaleDetails> saleDetails;
        public ObservableCollection<SaleDetails> SaleDetails
        {

            get { return this.saleDetails; }
            set { this.SetValue(ref this.saleDetails, value); }
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
        public SaleDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<SaleDetails>(
              url,
              "/api",
              "/SaleDetails",
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
            var mySaleDetails = (List<SaleDetails>)response.Result;
            this.SaleDetails = new ObservableCollection<SaleDetails>(mySaleDetails);
        }
    }
}
