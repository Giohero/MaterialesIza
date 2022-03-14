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
    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<ClientRequest> myClients;
        private ObservableCollection<ClientItemViewModel> clients;
        public ObservableCollection<ClientItemViewModel> Clients
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

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadClients();
        }

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
            myClients = (List<ClientRequest>)response.Result;
            RefreshClientsList();
        }

        private void RefreshClientsList()
        {
            this.Clients = new ObservableCollection<ClientItemViewModel>(myClients.Select(c => new ClientItemViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }).OrderBy(c => c.FirstName).ToList());
        }

        public void AddClientToList(ClientRequest client)
        {
            this.myClients.Add(client);
            RefreshClientsList();
        }

        public void UpdateClientInList(ClientRequest client)
        {
            var previousClient = myClients.Where(c => c.Id == client.Id).FirstOrDefault();
            if (previousClient != null)
            {
                this.myClients.Remove(previousClient);
            }
            this.myClients.Add(client);
            RefreshClientsList();
        }

        public void DeleteClientInList(int clientId)
        {
            var previousClient = myClients.Where(c => c.Id == clientId).FirstOrDefault();
            if (previousClient != null)
            {
                this.myClients.Remove(previousClient);
            }
            RefreshClientsList();
        }
    }
}

