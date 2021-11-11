using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Sale> sales;
        public ObservableCollection<Sale> Sales
        {

            get { return this.sales; }
            set { this.SetValue(ref this.sales, value); }
        }

        public SalesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Sale>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/Sales");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var mySales = (List<Sale>)response.Result;
            this.Sales = new ObservableCollection<Sale>(mySales);
        }
    }
}
