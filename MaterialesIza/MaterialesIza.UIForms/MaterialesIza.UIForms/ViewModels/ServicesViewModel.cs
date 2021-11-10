using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ServicesViewModel:BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Services
        {

            get { return this.services; }
            set { this.SetValue(ref this.services, value); }
        }

        public ServicesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadServices();
        }

        private async void LoadServices()
        {
            var response = await this.apiService.GetListAsync<Service>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/Services");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myServices = (List<Service>)response.Result;
            this.Services = new ObservableCollection<Service>(myServices);
        }
    }
}
