using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class SaleDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<SaleDetail> saleDetails;
        public ObservableCollection<SaleDetail> SaleDetails
        {

            get { return this.saleDetails; }
            set { this.SetValue(ref this.saleDetails, value); }
        }

        public SaleDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<SaleDetail>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/SaleDetails");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var mySaleDetails = (List<SaleDetail>)response.Result;
            this.SaleDetails = new ObservableCollection<SaleDetail>(mySaleDetails);
        }
    }
}
