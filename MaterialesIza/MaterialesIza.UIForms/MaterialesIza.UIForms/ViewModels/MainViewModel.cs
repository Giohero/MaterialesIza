
using MaterialesIza.Common.Models;

namespace MaterialesIza.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }

        public ProductsViewModel Products { get; set; }
        public MainViewModel()
        {
            instance = this;
            //this.Login = new LoginViewModel();
        }
        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
