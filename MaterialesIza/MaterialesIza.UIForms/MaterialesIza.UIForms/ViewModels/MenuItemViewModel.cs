using GalaSoft.MvvmLight.Command;
using MaterialesIza.UIForms.Views;
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

            switch (this.PageName)
            {
                case "ServicePage":
                    MainViewModel.GetInstance().Services = new ServicesViewModel();
                    await App.Navigator.PushAsync(new ServicePage());
                    break;

                case "ProductsPage":
                    MainViewModel.GetInstance().Products = new ProductsViewModel();
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;

                case "ServiceTypesPage":
                    await App.Navigator.PushAsync(new ServiceTypesPage());
                    break;

                case "ProductTypesPage":
                    MainViewModel.GetInstance().ProductTypes = new ProductTypesViewModel();
                    await App.Navigator.PushAsync(new ProductTypesPage());
                    break;

                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;

                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;

                case "ProvidersPage":
                    await App.Navigator.PushAsync(new ProvidersPage());
                    break;

                case "PurchasesPage":
                    await App.Navigator.PushAsync(new PurchasesPage());
                    break;

                case "PurchaseDetailsPage":
                    await App.Navigator.PushAsync(new PurchaseDetailsPage());
                    break;

                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;

                case "OrderDetailsPage":
                    await App.Navigator.PushAsync(new OrderDetailsPage());
                    break;
                case "AdminsPage":
                    await App.Navigator.PushAsync(new AdminsPage());
                    break;

                case "ClientsPage":
                    MainViewModel.GetInstance().Clients = new ClientsViewModel();
                    await App.Navigator.PushAsync(new ClientsPage());
                    break;

                case "EmployeesPage":
                    await App.Navigator.PushAsync(new EmployeesPage());
                    break;

                case "SalesPage":
                    await App.Navigator.PushAsync(new SalesPage());
                    break;

                case "SaleDetailsPage":
                    await App.Navigator.PushAsync(new SaleDetailsPage());
                    break;

                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
