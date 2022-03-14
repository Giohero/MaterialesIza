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
    public class ProviderTypesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private List<ProviderTypeRequest> myProviderTypes;

        private ObservableCollection<ProviderItemViewModel> providerTypes;
        public ObservableCollection<ProviderItemViewModel> ProviderTypes
        {

            get { return this.providerTypes; }
            set { this.SetValue(ref this.providerTypes, value); }
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
            this.LoadProviderTypes();
        }
        public ProviderTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProviderTypes();
        }

        private async void LoadProviderTypes()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProviderTypeRequest>(
              url,
              "/api",
              "/ProviderTypes",
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
            myProviderTypes = (List<ProviderTypeRequest>)response.Result;
            RefreshProviderTypesList();

        }

        private void RefreshProviderTypesList()
        {
            this.ProviderTypes = new ObservableCollection<ProviderItemViewModel>(myProviderTypes.Select(pv => new ProviderItemViewModel
            {
                Id = pv.Id,
                FirstName = pv.FirstName
            }).OrderBy(pv => pv.FirstName).ToList());
        }
        public void AddProviderTypeToList(ProviderTypeRequest providerType)
        {
            this.myProviderTypes.Add(providerType);
            RefreshProviderTypesList();
        }
        public void UpdateProviderTypeToList(ProviderTypeRequest providerType)
        {
            var previousProviderType = myProviderTypes.Where(pv => pv.Id == providerType.Id).FirstOrDefault();
            if (previousProviderType != null)
            {
                this.myProviderTypes.Remove(previousProviderType);
            }
            this.myProviderTypes.Add(providerType);
            RefreshProviderTypesList();
        }
        public void DeleteProviderTypeInList(int providerTypeId)
        {
            var previousProviderType = myProviderTypes.Where(pv => pv.Id == providerTypeId).FirstOrDefault();
            if (previousProviderType != null)
            {
                this.myProviderTypes.Remove(previousProviderType);
            }
            RefreshProviderTypesList();
        }
    }
}
