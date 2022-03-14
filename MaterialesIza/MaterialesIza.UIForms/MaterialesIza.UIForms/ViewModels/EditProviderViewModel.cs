using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EditProviderViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public ProviderRequest Provider { get; set; }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
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
                "/Providers",
                Provider.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Providers.DeleteProviderInList(Provider.Id);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Provider.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Prooveedor", "Aceptar");
                return;
            }
   
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/Providers",
                Provider.Id,
                Provider,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyProvider = (ProviderRequest)response.Result;
            MainViewModel.GetInstance().Providers.UpdateProviderInList(modifyProvider);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }
        public EditProviderViewModel(ProviderRequest provider)
        {
            this.Provider = provider;
            this.apiService = new ApiService();
            this.isEnabled = true;
       

        }
        


    }
}
