using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProvidersViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Provider> providers;
        public ObservableCollection<Provider> Providers
        {

            get { return this.providers; }
            set { this.SetValue(ref this.providers, value); }
        }

        public ProvidersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Provider>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/Providers");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myProviders = (List<Provider>)response.Result;
            this.Providers = new ObservableCollection<Provider>(myProviders);
        }
    }
}
