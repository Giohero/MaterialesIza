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

        //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProductTypes();
        }

        private async void LoadProductTypes()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProducType>(
              url,
              "/api",
              "/ProductTypes",
              "bearer",
              MainViewModel.GetInstance().Token.Token);
            //Final de carga
            this.IsRefreshing = false;
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
