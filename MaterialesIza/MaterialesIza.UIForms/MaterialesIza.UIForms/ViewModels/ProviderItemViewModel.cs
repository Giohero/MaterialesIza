using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProviderItemViewModel : ProviderRequest
    {
        public ICommand SelectProviderCommand { get { return new RelayCommand(SelectProvider); } }

        private async void SelectProvider()
        {
            MainViewModel.GetInstance().EditProvider = new EditProviderViewModel((ProviderRequest)this);
            await App.Navigator.PushAsync(new EditProviderPage());
        }
    }
}