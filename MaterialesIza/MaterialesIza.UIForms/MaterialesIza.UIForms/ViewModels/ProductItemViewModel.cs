using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProductItemViewModel : ProductRequest
    {
        public ICommand SelectProductCommand { get { return new RelayCommand(SelectProduct); } }

        private async void SelectProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel((ProductRequest)this);
            await App.Navigator.PushAsync(new EditProductPage());
        }
    }
}
