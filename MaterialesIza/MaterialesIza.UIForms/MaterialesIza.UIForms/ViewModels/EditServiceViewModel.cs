using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EditServiceViewModel:BaseViewModel
    {
        private readonly ApiService apiService;
        public ServiceRequest ServiceRequest { get; set; }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        private IList<string> serviceTypeList;
        public IList<string> ServiceTypeList
        {
            get { return this.serviceTypeList; }
            set { this.SetValue(ref this.serviceTypeList, value); }
        }
        private async void LoadServicesTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ServiceTypeRequest>(
                url,
                "/api",
                "/ServiceTypes",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            ServiceTypeList = ((List<ServiceTypeRequest>)response.Result).Select(m => m.TypeService).ToList();

        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }
        public ICommand DeleteCommand { get { return new RelayCommand(Delete); } }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Seguro de eliminar", "SI", "NO");
            if (!confirm)
                return;
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(url,
                "/api",
                "/Products",
                ServiceRequest.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Services.DeleteServiceInList(ServiceRequest.Id);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.ServiceRequest.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Servicio", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.ServiceRequest.Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una descripcion", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.ServiceRequest.ServiceType))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Servicio", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/Products",
                ServiceRequest.Id,
                ServiceRequest,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyService = (ServiceRequest)response.Result;
            MainViewModel.GetInstance().Services.UpdateServiceInList(modifyService);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public EditServiceViewModel(ServiceRequest service)
        {
            this.ServiceRequest = service;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.LoadServicesTypes();

        }
    }
}
