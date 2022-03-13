using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EmployeeItemViewModel : EmployeeRequest
    {
        public ICommand SelectEmployeeCommand { get { return new RelayCommand(SelectEmployee); } }

        private async void SelectEmployee()
        {
            MainViewModel.GetInstance().EditEmployee = new EditEmployeeViewModel((EmployeeRequest)this);
            await App.Navigator.PushAsync(new EditEmployeePage());
        }
    }
}
