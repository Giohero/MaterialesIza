using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class EditPurchaseViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public PurchaseRequest PurchaseRequest { get; set; }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }
        public ICommand DeleteCommand { get { return new RelayCommand(Delete); } }
        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Seguro de eliminar", "SI", "NO");
            if (!confirm)
                return;
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(url,
                "/api",
                "/Purchases",
                PurchaseRequest.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Purchases.DeletePurchaseInList(PurchaseRequest.Id);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(PurchaseRequest.PurchaseName))//¿PurchaseRequest.Id?
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes introducir alguna Compra", "Aceptar");
                return;
            }
            isEnabled = false;
            isRunning = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(url,
                "/api",
                "/Purchases",
                PurchaseRequest.Id,
                PurchaseRequest,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var modifyPurchase = (PurchaseRequest)response.Result;
            MainViewModel.GetInstance().Purchases.UpdatePurchaseInList(modifyPurchase);
            isEnabled = true;
            isRunning = false;
            await App.Navigator.PopAsync();
        }
        public EditPurchaseViewModel(PurchaseRequest purchase)
        {
            this.PurchaseRequest = purchase;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.LoadPurchaseTypes();

        }
        private IList<string> purchaseTypeList;
        public IList<string> PurchaseTypeList
        {
            get { return this.purchaseTypeList; }
            set { this.SetValue(ref this.purchaseTypeList, value); }
        }
        private async void LoadPurchaseTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseTypeRequest>(
                url,
                "/api",
                "/PurchaseTypes",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            PurchaseTypeList = ((List<PurchaseTypeRequest>)response.Result).Select(pu => pu.PurchaseName).ToList();

        }

    }
}
