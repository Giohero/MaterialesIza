using MaterialesIza.UIForms.ViewModels;
using MaterialesIza.UIForms.Views;
using Xamarin.Forms;

namespace MaterialesIza.UIForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
