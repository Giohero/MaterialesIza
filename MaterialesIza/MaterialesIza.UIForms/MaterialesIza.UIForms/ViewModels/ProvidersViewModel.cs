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
    public class ProvidersViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<ProviderRequest> myProviders;

        private ObservableCollection<ProviderItemViewModel> providers;
        public ObservableCollection<ProviderItemViewModel> Providers
        {

            get { return this.providers; }
            set { this.SetValue(ref this.providers, value); }
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
            this.LoadProducts();
        }
        public ProvidersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProviderRequest>(
              url,
              "/api",
              "/Providers",
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
            //var myProviders = (List<ProviderRequest>)response.Result;
            //this.Providers = new ObservableCollection<ProviderRequest>(myProviders);
            myProviders = (List<ProviderRequest>)response.Result;
            RefreshProvidersList();
        }
        private void RefreshProvidersList()
        {
            this.Providers = new ObservableCollection<ProviderItemViewModel>(myProviders.Select(pv => new ProviderItemViewModel
            {
                Id = pv.Id,
                FirstName = pv.FirstName,
                LastName = pv.LastName,
                Email = pv.Email,
                PhoneNumber = pv.PhoneNumber
            }).OrderBy(pv => pv.FirstName).ToList());
        }
        public void AddProviderToList(ProviderRequest provider)
        {
            this.myProviders.Add(provider);
            RefreshProvidersList();
        }

        public void UpdateProviderInList(ProviderRequest provider)
        {
            var previousProvider = myProviders.Where(pv => pv.Id == provider.Id).FirstOrDefault();

            if (previousProvider != null)
            {
                this.myProviders.Remove(previousProvider);
            }
            this.myProviders.Add(provider);
            RefreshProvidersList();
        }
        public void DeleteProviderInList(int providerId)
        {
            var previousProvider = myProviders.Where(pv => pv.Id == providerId).FirstOrDefault();

            if (previousProvider != null)
            {
                this.myProviders.Remove(previousProvider);
            }
            RefreshProvidersList();
        }
    }

}