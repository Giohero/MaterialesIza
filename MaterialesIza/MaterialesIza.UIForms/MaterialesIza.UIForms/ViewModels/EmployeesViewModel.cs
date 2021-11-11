using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {

            get { return this.employees; }
            set { this.SetValue(ref this.employees, value); }
        }

        public EmployeesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Employee>(
               "https://materialesiza20211109222312.azurewebsites.net", "/api", "/Employees");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myEmployees = (List<Employee>)response.Result;
            this.Employees = new ObservableCollection<Employee>(myEmployees);
        }
    }
}
