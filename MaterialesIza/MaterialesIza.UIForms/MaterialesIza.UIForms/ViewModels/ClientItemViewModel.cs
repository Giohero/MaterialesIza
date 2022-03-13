using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ClientItemViewModel : ClientRequest
    {
        public ICommand SelectClientCommand { get { return new RelayCommand(SelectClient); } }

        private async void SelectClient()
        {
            MainViewModel.GetInstance().EditClient = new EditClientViewModel((ClientRequest)this);
            await App.Navigator.PushAsync(new EditClientPage());
        }
    }
}
