using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<EmployeeRequest> myEmployees;
        private ObservableCollection<EmployeeItemViewModel> employees;
        public ObservableCollection<EmployeeItemViewModel> Employees
        {
            get { return this.employees; }
            set { this.SetValue(ref this.employees, value); }
        }

        //propiedades de recarga
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            this.LoadEmployees();
        }

        public EmployeesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEmployees();
        }

        private async void LoadEmployees()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<EmployeeRequest>(
              url,
              "/api",
              "/Employees",
              "bearer",
              MainViewModel.GetInstance().Token.Token);
            //Final de carga
            this.IsRefreshing = false;
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            myEmployees = (List<EmployeeRequest>)response.Result;
            RefreshEmployeesList();
        }

        private void RefreshEmployeesList()
        {
            this.Employees = new ObservableCollection<EmployeeItemViewModel>(myEmployees.Select(e => new EmployeeItemViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).OrderBy(e => e.FirstName).ToList());
        }

        public void AddEmployeeToList(EmployeeRequest employee)
        {
            this.myEmployees.Add(employee);
            RefreshEmployeesList();
        }

        public void UpdateEmployeeInList(EmployeeRequest employee)
        {
            var previousEmployee = myEmployees.Where(e => e.Id == employee.Id).FirstOrDefault();
            if (previousEmployee != null)
            {
                this.myEmployees.Remove(previousEmployee);
            }
            this.myEmployees.Add(employee);
            RefreshEmployeesList();
        }

        public void DeleteEmployeeInList(int employeeId)
        {
            var previousEmployee = myEmployees.Where(e => e.Id == employeeId).FirstOrDefault();
            if (previousEmployee != null)
            {
                this.myEmployees.Remove(previousEmployee);
            }
            RefreshEmployeesList();
        }
    }
}

