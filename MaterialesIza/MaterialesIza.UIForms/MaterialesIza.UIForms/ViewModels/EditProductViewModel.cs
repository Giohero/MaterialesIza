using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EditProductViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public ProductRequest ProductRequest { get; set; }

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
        public ICommand DeleteCommand { get { return new RelayCommand(Delete); } }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Seguro de eliminar", "SI", "NO");
            if (!confirm)
                return;
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(url,
                "/api",
                "/Products",
                ProductRequest.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Products.DeleteProductInList(ProductRequest.Id);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.ProductRequest.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Producto", "Aceptar");
                return;
            }
            //if (string.IsNullOrEmpty(this.ProductRequest.Price.ToString()))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Precio", "Aceptar");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.ProductRequest.Description))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir una descripcion", "Aceptar");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.ProductRequest.ProductTypes))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir un Tipo de Producto", "Aceptar");
            //    return;
            //}

            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/Products",
                ProductRequest.Id,
                ProductRequest,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyProduct = (ProductRequest)response.Result;
            MainViewModel.GetInstance().Products.UpdateProductInList(modifyProduct);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        public EditProductViewModel(ProductRequest product)
        {
            this.ProductRequest = product;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.LoadProductTypes();

        }
    }
}
