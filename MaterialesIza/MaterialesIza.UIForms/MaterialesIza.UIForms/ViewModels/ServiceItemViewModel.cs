using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ServiceItemViewModel : ServiceRequest
    {
        public ICommand SelectServiceCommand { get { return new RelayCommand(SelectService); } }

        private async void SelectService()
        {
            MainViewModel.GetInstance().EditService = new EditServiceViewModel((ServiceRequest)this);
            await App.Navigator.PushAsync(new EditServicePage());
        }
    }
}
