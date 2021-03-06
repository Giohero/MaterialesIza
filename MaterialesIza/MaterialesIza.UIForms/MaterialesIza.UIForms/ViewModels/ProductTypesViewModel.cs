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
    public class ProductTypesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private List<ProductTypeRequest> myProductTypes;

        private ObservableCollection<ProductTypeItemViewModel> productTypes;
        public ObservableCollection<ProductTypeItemViewModel> ProductTypes
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
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadProductTypes();
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
            var response = await this.apiService.GetListAsync<ProductTypeRequest>(
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
            myProductTypes = (List<ProductTypeRequest>)response.Result;
            RefreshProductTypesList();
            
        }

        private void RefreshProductTypesList()
        {
            this.ProductTypes = new ObservableCollection<ProductTypeItemViewModel>(myProductTypes.Select(pt => new ProductTypeItemViewModel
            {
                Id = pt.Id,
                Name = pt.Name
            }).OrderBy(pt => pt.Name).ToList());
        }
        public void AddProductTypeToList(ProductTypeRequest productType)
        {
            this.myProductTypes.Add(productType);
            RefreshProductTypesList();
        }
        public void UpdateProductTypeToList(ProductTypeRequest productType)
        {
            var previousProductType = myProductTypes.Where(pt => pt.Id == productType.Id).FirstOrDefault();
            if (previousProductType != null)
            {
                this.myProductTypes.Remove(previousProductType);
            }
            this.myProductTypes.Add(productType);
            RefreshProductTypesList();
        }
        public void DeleteProductTypeInList(int productTypeId)
        {
            var previousProductType = myProductTypes.Where(pt => pt.Id == productTypeId).FirstOrDefault();
            if (previousProductType != null)
            {
                this.myProductTypes.Remove(previousProductType);
            }
            RefreshProductTypesList();
        }
    }
}
