﻿using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
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

        public ClientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Client>(
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
            var myClients = (List<Client>)response.Result;
            this.Clients = new ObservableCollection<Client>(myClients);
        }
    }
}
