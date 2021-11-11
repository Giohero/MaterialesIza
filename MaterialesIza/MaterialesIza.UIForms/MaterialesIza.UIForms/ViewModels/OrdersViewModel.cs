using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {

            get { return this.orders; }
            set { this.SetValue(ref this.orders, value); }
        }

        public OrdersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Order>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/Orders");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myOrders = (List<Order>)response.Result;
            this.Orders = new ObservableCollection<Order>(myOrders);
        }
    }
}
