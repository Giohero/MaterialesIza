﻿using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class SaleDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<SaleDetails> saleDetails;
        public ObservableCollection<SaleDetails> SaleDetails
        {

            get { return this.saleDetails; }
            set { this.SetValue(ref this.saleDetails, value); }
        }

        public SaleDetailsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<SaleDetails>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/SaleDetails");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var mySaleDetails = (List<SaleDetails>)response.Result;
            this.SaleDetails = new ObservableCollection<SaleDetails>(mySaleDetails);
        }
    }
}