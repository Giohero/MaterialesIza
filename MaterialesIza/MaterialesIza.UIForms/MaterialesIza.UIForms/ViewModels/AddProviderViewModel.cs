using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Services;
using System.Windows.Input;
using Xamarin.Forms;
using MaterialesIza.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddProviderViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        private IList<string> providerTypeList;
        public IList<string> ProviderTypeList
        {
            get { return this.providerTypeList; }
            set { this.SetValue(ref this.providerTypeList, value); }
        }
        private async void LoadProviderTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProviderTypeRequest>(//¿¿¿ProviderTypeRequest???
                url,
                "/api",
                "/ProviderTypes",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            ProviderTypeList = ((List<ProviderTypeRequest>)response.Result).Select(pv => pv.FirstName).ToList();

        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir el nombre del Proveedor", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(LastName.ToString())) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir el apellido del Proveedor", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir el email del Proveedor", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir el numero de telefono del Proveedor", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var provider = new ProviderRequest { FirstName = FirstName, LastName = LastName, Email = Email, PhoneNumber = PhoneNumber };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Providers",
                provider,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newProvider = (ProviderRequest)response.Result;
            MainViewModel.GetInstance().Providers.AddProviderToList(newProvider);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddProviderViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
            this.LoadProviderTypes();
        }
    }
}