using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
        {

            get { return this.clients; }
            set { this.SetValue(ref this.clients, value); }
        }

        public ClientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Client>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/Clients");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myClients = (List<Client>)response.Result;
            this.Clients = new ObservableCollection<Client>(myClients);
        }
    }
}
