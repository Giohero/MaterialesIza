using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProductTypeItemViewModel : ProductTypeRequest
    {
        public ICommand SelectProductTypeCommand { get { return new RelayCommand(SelectProductType); } }

        private async void SelectProductType()
        {
            MainViewModel.GetInstance().EditProductType = new EditProductTypeViewModel((ProductTypeRequest)this);
            await App.Navigator.PushAsync(new EditProductTypesPage());
        }
    }
}
