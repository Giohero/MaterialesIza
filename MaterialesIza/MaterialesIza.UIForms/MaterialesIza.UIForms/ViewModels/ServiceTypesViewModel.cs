using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace MaterialesIza.UIForms.ViewModels
{
    public class ServiceTypesViewModel:BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<ServiceType> serviceTypes;
        public ObservableCollection<ServiceType> ServiceTypes
        {

            get { return this.serviceTypes; }
            set { this.SetValue(ref this.serviceTypes, value); }
        }

        public ServiceTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadServiceTypes();
        }

        private async void LoadServiceTypes()
        {
            var response = await this.apiService.GetListAsync<ServiceType>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/ServiceTypes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myServiceTypes = (List<ServiceType>)response.Result;
            this.ServiceTypes = new ObservableCollection<ServiceType>(myServiceTypes);
        }
    }
}
