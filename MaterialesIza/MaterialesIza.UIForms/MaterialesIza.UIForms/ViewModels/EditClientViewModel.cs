using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EditClientViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public ClientRequest Client { get; set; }

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
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Seguro que deseas eleminar?", "SI", "NO");
            if (!confirm)
                return;

            isEnabled = false;
            isRunning = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(url,
                "/api",
                "/Clients",
                Client.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Clients.DeleteClientInList(Client.Id);
            this.isEnabled = true;
            this.isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Client.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un nombre", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Client.LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un apellido", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Client.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un correo electrónico", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Client.PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un número telefónico", "Aceptar");
                return;
            }
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/Clients",
                Client.Id,
                Client,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyClient = (ClientRequest)response.Result;
            MainViewModel.GetInstance().Clients.UpdateClientInList(modifyClient);
            this.isEnabled = true;
            this.isRunning = false;
            await App.Navigator.PopAsync();
        }

        public EditClientViewModel(ClientRequest client)
        {
            this.Client = client;
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
    }
}
