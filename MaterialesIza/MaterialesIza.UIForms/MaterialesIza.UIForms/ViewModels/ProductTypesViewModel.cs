using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProductTypesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<ProducType> productTypes;
        public ObservableCollection<ProducType> ProductTypes
        {

            get { return this.productTypes; }
            set { this.SetValue(ref this.productTypes, value); }
        }

        public ProductTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProductTypes();
        }

        private async void LoadProductTypes()
        {
            var response = await this.apiService.GetListAsync<ProducType>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/ProductTypes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myProductTypes = (List<ProducType>)response.Result;
            this.ProductTypes = new ObservableCollection<ProducType>(myProductTypes);
        }
    }
}
