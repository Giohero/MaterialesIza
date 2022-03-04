using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<ClientRequest> clients;
        public ObservableCollection<ClientRequest> Clients
        {

            get { return this.clients; }
            set { this.SetValue(ref this.clients, value); }
        }
        
         //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        //public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        //private void Refresh()
        //{
        //    this.LoadClients();
        //}

        public ClientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadClients();
        }

        private async void LoadClients()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ClientRequest>(
              url,
              "/api",
              "/Clients",
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
            var myClients = (List<ClientRequest>)response.Result;
            this.Clients = new ObservableCollection<ClientRequest>(myClients);
        }
    }
}
