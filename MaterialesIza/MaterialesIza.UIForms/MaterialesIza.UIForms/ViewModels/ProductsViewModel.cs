using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<ProductRequest> myProducts;
        private ObservableCollection<ProductItemViewModel> products;
        public ObservableCollection<ProductItemViewModel> Products
        {

            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadProducts();
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<ProductRequest>(
              url,
              "/api",
              "/Products",
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
            myProducts = (List<ProductRequest>)response.Result;
            RefreshProductsList();
        }

        private void RefreshProductsList()
        {
            this.Products = new ObservableCollection<ProductItemViewModel>(myProducts.Select(p => new ProductItemViewModel
                { 
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ProductTypes = p.ProductTypes
                }
                ).OrderBy(p => p.Name).ToList());
        }
        public void AddProductToList(ProductRequest product)
        {
            this.myProducts.Add(product);
            RefreshProductsList();
        }

        public void UpdateProductInList(ProductRequest product)
        {
            var previousProduct = myProducts.Where(p => p.Id == product.Id).FirstOrDefault();

            if(previousProduct != null)
            {
                this.myProducts.Remove(previousProduct);
            }
            this.myProducts.Add(product);
            RefreshProductsList();
        }
        public void DeleteProductInList(int productId)
        {
            var previousProduct = myProducts.Where(p => p.Id == productId).FirstOrDefault();

            if (previousProduct != null)
            {
                this.myProducts.Remove(previousProduct);
            }
            RefreshProductsList();
        }
    }
}
