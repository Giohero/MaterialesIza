using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ServicesViewModel:BaseViewModel
    {
        private ApiService apiService;
        private List<ServiceRequest> myServices;

        private ObservableCollection<ServiceItemViewModel> services;
        public ObservableCollection<ServiceItemViewModel> Services
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
            myServices = (List<ServiceRequest>)response.Result;
            RefreshServicesList();
        }
        private void RefreshServicesList()
        {
            this.Services = new ObservableCollection<ServiceItemViewModel>
                (myServices.Select(p => new ServiceItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ServiceType = p.ServiceType
                }
                ).OrderBy(p => p.ServiceType).ToList());
        }
        public void AddServiceToList(ServiceRequest service)
        {
            this.myServices.Add(service);
            RefreshServicesList();
        }

        public void UpdateServiceInList(ServiceRequest service)
        {
            var previousService = myServices.Where(p => p.Id == service.Id).FirstOrDefault();

            if (previousService != null)
            {
                this.myServices.Remove(previousService);
            }
            this.myServices.Add(service);
            RefreshServicesList();
        }
        public void DeleteServiceInList(int serviceId)
        {
            var previousService = myServices.Where(s => s.Id == serviceId).FirstOrDefault();

            if (previousService != null)
            {
                this.myServices.Remove(previousService);
            }
            RefreshServicesList();
        }
    }
}
