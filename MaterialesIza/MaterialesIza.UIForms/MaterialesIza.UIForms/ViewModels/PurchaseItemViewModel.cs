using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchaseItemViewModel : PurchaseRequest
    {
        public ICommand SelectPurchaseCommand { get { return new RelayCommand(SelectPurchase); } }

        private async void SelectPurchase()
        {
            MainViewModel.GetInstance().EditPurchase = new EditPurchaseViewModel((PurchaseRequest)this);
            await App.Navigator.PushAsync(new EditPurchasePage());
        }
    }
}
