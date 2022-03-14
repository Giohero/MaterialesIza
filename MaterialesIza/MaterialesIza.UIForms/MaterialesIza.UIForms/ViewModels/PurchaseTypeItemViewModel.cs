using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchaseTypeItemViewModel : PurchaseTypeRequest
    {
        public ICommand SelectPurchaseTypeCommand { get { return new RelayCommand(SelectPurchaseType); } }

        private async void SelectPurchaseType()
        {
            MainViewModel.GetInstance().EditPurchaseType = new EditPurchaseTypeViewModel((PurchaseTypeRequest)this);
            await App.Navigator.PushAsync(new EditPurchaseTypePage());
        }
    }
}
