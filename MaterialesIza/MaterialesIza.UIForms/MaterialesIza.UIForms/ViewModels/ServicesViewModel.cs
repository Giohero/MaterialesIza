using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ServicesViewModel:BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<ServiceRequest> services;
        public ObservableCollection<ServiceRequest> Services
        {

            get { return this.services; }
            set { this.SetValue(ref this.services, value); }
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
            this.LoadServices();
        }

        public ServicesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadServices();
        }

        private async void LoadServices()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ServiceRequest>(
              url,
              "/api",
              "/Services",
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
            var myServices = (List<ServiceRequest>)response.Result;
            this.Services = new ObservableCollection<ServiceRequest>(myServices);
        }
    }
}
