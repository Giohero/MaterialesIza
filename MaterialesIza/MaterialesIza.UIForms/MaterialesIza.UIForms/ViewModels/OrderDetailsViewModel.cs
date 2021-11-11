using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<OrderDetail> orderDetails;
        public ObservableCollection<OrderDetail> OrderDetails
        {

            get { return this.orderDetails; }
            set { this.SetValue(ref this.orderDetails, value); }
        }

        public OrderDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<OrderDetail>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/OrderDetails");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myOrderDetails = (List<OrderDetail>)response.Result;
            this.OrderDetails = new ObservableCollection<OrderDetail>(myOrderDetails);
        }
    }
}
