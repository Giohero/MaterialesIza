using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchaseTypesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private List<PurchaseTypeRequest> myPurchaseTypes;

        private ObservableCollection<PurchaseTypeItemViewModel> purchaseTypes;
        public ObservableCollection<PurchaseTypeItemViewModel> PurchaseTypes
        {

            get { return this.purchaseTypes; }
            set { this.SetValue(ref this.purchaseTypes, value); }
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
            this.LoadPurchaseTypes();
        }
        public PurchaseTypesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPurchaseTypes();
        }

        private async void LoadPurchaseTypes()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseTypeRequest>(
              url,
              "/api",
              "/PurchaseTypes",
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
            myPurchaseTypes = (List<PurchaseTypeRequest>)response.Result;
            RefreshPurchaseTypesList();

        }

        private void RefreshPurchaseTypesList()
        {
            this.PurchaseTypes = new ObservableCollection<PurchaseTypeItemViewModel>(myPurchaseTypes.Select(pu => new PurchaseTypeItemViewModel
            {
                Id = pu.Id,
                PurchaseName = pu.PurchaseName
            }).OrderBy(pu => pu.PurchaseName).ToList());
        }
        public void AddPurchaseTypeToList(PurchaseTypeRequest purchaseType)
        {
            this.myPurchaseTypes.Add(purchaseType);
            RefreshPurchaseTypesList();
        }
        public void UpdatePurchaseTypeToList(PurchaseTypeRequest purchaseType)
        {
            var previousPurchaseType = myPurchaseTypes.Where(pu => pu.Id == purchaseType.Id).FirstOrDefault();
            if (previousPurchaseType != null)
            {
                this.myPurchaseTypes.Remove(previousPurchaseType);
            }
            this.myPurchaseTypes.Add(purchaseType);
            RefreshPurchaseTypesList();
        }
        public void DeletePurchaseTypeInList(int purchaseTypeId)
        {
            var previousPurchaseType = myPurchaseTypes.Where(pu => pu.Id == purchaseTypeId).FirstOrDefault();
            if (previousPurchaseType != null)
            {
                this.myPurchaseTypes.Remove(previousPurchaseType);
            }
            RefreshPurchaseTypesList();
        }
    }
}
