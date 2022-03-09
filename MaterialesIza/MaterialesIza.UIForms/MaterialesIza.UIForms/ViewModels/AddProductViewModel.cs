using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Services;
using System.Windows.Input;
using Xamarin.Forms;
using MaterialesIza.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace MaterialesIza.UIForms.ViewModels
{
    public class AddProductViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public string Name { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }

        public string ProductTypes { get; set; }


        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        private IList<string> productTypeList;
        public IList<string> ProductTypeList
        {
            get { return this.productTypeList; }
            set { this.SetValue(ref this.productTypeList, value); }
        }
        private async void LoadProductTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProductTypeRequest>(
                url,
                "/api",
                "/ProductTypes",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            ProductTypeList = ((List<ProductTypeRequest>)response.Result).Select(m => m.Name).ToList();

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
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Producto", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Price.ToString())) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Precio", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una descripcion", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(ProductTypes))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Producto", "Aceptar");
                return;
            }

            isEnabled = false;
            isRunning = true;
            var product = new ProductRequest { Name = Name, Price = Price, Description = Description, ProductTypes = ProductTypes };
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(url,
                "/api",
                "/Products",
                product,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var newProduct = (ProductRequest)response.Result;
            MainViewModel.GetInstance().Products.Products.Add(newProduct);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            isEnabled = true;
            this.LoadProductTypes();
        }
    }
}
