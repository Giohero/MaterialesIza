using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class ServiceTypeItemViewModel : ServiceTypeRequest
    {
        public ICommand SelectServiceTypeCommand { get { return new RelayCommand(SelectServiceType); } }

        private async void SelectServiceType()
        {
            MainViewModel.GetInstance().EditServiceType = new EditServiceTypeViewModel((ServiceTypeRequest)this);
            await App.Navigator.PushAsync(new EditServiceTypesPage());
        }
    }
}
