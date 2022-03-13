using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Services;
using System.Windows.Input;
using Xamarin.Forms;
using MaterialesIza.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
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
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un nombre", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un apellido", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un corrro electrónico", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un número telefónico", "Aceptar");
                return;
            }


            isEnabled = false;
            isRunning = true;
            var employee = new EmployeeRequest { FirstName = FirstName, LastName = LastName, Email = Email, PhoneNumber = PhoneNumber };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Employees",
                employee,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newEmployee = (EmployeeRequest)response.Result;
            MainViewModel.GetInstance().Employees.AddEmployeeToList(newEmployee);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }
        public AddEmployeeViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
        }
    }
}
