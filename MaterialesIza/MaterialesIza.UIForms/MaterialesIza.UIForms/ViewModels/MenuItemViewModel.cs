using GalaSoft.MvvmLight.Command;
using MaterialesIza.UIForms.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class MenuItemViewModel : MaterialesIza.Common.Models.Menu
    {
        public ICommand SelectMenuCommand { get { return new RelayCommand(SelectMenu); } }

        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            //var mainViewModel = MainViewModel.GetInstance();

            switch(this.PageName)
            {
                case "ServicePage":
                    await App.Navigator.PushAsync(new ServicePage());
                    break;

                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;

                case "ServiceTypesPage":
                    await App.Navigator.PushAsync(new ServiceTypesPage());
                    break;

                case "ProductsTypePage":
                    await App.Navigator.PushAsync(new ProductTypesPage());
                    break;

                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;

                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
