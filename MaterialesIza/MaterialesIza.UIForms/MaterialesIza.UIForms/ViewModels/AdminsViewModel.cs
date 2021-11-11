using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AdminsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Admin> admins;
        public ObservableCollection<Admin> Admins
        {

            get { return this.admins; }
            set { this.SetValue(ref this.admins, value); }
        }

        public AdminsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Admin>(
               "https://materialesiza20211111035147.azurewebsites.net", "/api", "/Admins");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myAdmins = (List<Admin>)response.Result;
            this.Admins = new ObservableCollection<Admin>(myAdmins);
        }
    }
}
