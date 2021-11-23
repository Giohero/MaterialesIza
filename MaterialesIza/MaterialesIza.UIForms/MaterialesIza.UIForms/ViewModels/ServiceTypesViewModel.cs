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

        //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ServiceTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadServiceTypes();
        }

        private async void LoadServiceTypes()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ServiceType>(
              url,
              "/api",
              "/ServiceTypes",
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
            var myServiceTypes = (List<ServiceType>)response.Result;
            this.ServiceTypes = new ObservableCollection<ServiceType>(myServiceTypes);
        }
    }
}
