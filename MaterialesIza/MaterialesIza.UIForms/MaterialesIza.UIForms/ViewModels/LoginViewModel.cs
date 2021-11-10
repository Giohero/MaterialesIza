using GalaSoft.MvvmLight.Command;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        public LoginViewModel()
        {
            this.Email = "yairz.@gmail.com";
            this.Password = "123456";
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Email", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una contraseña", "Aceptar");
                return;
            }
            if (!this.Email.Equals("yairz.@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email o contraseña incorrecta", "Aceptar");
                return;
            }
            //await Application.Current.MainPage.DisplayAlert("OK", "LIIISTO", "Aceptar");
            //return;

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

            MainViewModel.GetInstance().ProductTypes = new ProductTypesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductTypesPage());

            MainViewModel.GetInstance().Services = new ServicesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ServicePage());

            MainViewModel.GetInstance().ServiceTypes = new ServiceTypesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceTypesPage());
        }
    }
}
