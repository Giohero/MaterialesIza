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
    public class ServiceTypesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private List<ServiceTypeRequest> myServiceTypes;

        private ObservableCollection<ServiceTypeItemViewModel> serviceTypes;
        public ObservableCollection<ServiceTypeItemViewModel> ServiceTypes
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
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadServiceTypes();
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
            var response = await this.apiService.GetListAsync<ServiceTypeRequest>(
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
            myServiceTypes = (List<ServiceTypeRequest>)response.Result;
            RefreshServiceTypesList();
        }

        private void RefreshServiceTypesList()
        {
            this.ServiceTypes = new ObservableCollection<ServiceTypeItemViewModel>(myServiceTypes.Select(st => new ServiceTypeItemViewModel
                {
                    Id = st.Id,
                    TypeService = st.TypeService
                }).OrderBy(st => st.TypeService).ToList());
        }

        public void AddServiceTypeToList(ServiceTypeRequest serviceType)
        {
            this.myServiceTypes.Add(serviceType);
            RefreshServiceTypesList();
        }
        public void UpdateServiceTypeToList(ServiceTypeRequest serviceType)
        {
            var previousServiceType = myServiceTypes.Where(st => st.Id == serviceType.Id).FirstOrDefault();
            if (previousServiceType != null)
            {
                this.myServiceTypes.Remove(previousServiceType);
            }
            this.myServiceTypes.Add(serviceType);
            RefreshServiceTypesList();
        }
        public void DeleteServiceTypeInList(int serviceTypeId)
        {
            var previousServiceType = myServiceTypes.Where(st => st.Id == serviceTypeId).FirstOrDefault();
            if (previousServiceType != null)
            {
                this.myServiceTypes.Remove(previousServiceType);
            }
            RefreshServiceTypesList();
        }
    }
}
