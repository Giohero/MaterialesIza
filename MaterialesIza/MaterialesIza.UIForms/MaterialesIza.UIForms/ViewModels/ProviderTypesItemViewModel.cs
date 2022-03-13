using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ProviderTypeItemViewModel : ProviderTypeRequest
    {
        public ICommand SelectProviderTypeCommand { get { return new RelayCommand(SelectProviderType); } }

        private async void SelectProviderType()
        {
            MainViewModel.GetInstance().EditProviderType = new EditProviderTypeViewModel((ProviderTypeRequest)this);
            await App.Navigator.PushAsync(new EditProviderTypesPage());
        }
    }
}
