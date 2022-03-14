using GalaSoft.MvvmLight.Command;
using MaterialesIza.Common.Models;
using MaterialesIza.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;


namespace MaterialesIza.UIForms.ViewModels
{
    public class PurchasesViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<PurchaseRequest> myPurchases;

        private ObservableCollection<PurchaseItemViewModel> purchases;
        public ObservableCollection<PurchaseItemViewModel> Purchases
        {

            get { return this.purchases; }
            set { this.SetValue(ref this.purchases, value); }
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
            this.LoadPurchases();
        }
        public PurchasesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPurchases();
        }

        private async void LoadPurchases()
        {
            //Inicio
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<PurchaseRequest>(
              url,
              "/api",
              "/Purchases",
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
            myPurchases = (List<PurchaseRequest>)response.Result;
           // this.Purchases = new ObservableCollection<PurchaseRequest>(myPurchases);
            RefreshPurchasesList();
        }
        private void RefreshPurchasesList()
        {
            this.Purchases = new ObservableCollection<PurchaseItemViewModel>(myPurchases.Select(pu => new PurchaseItemViewModel
            {
                Id = pu.Id,
                Date_purchase = pu.Date_purchase,
                Total_purchase = pu.Total_purchase,
                Iva_purchase = pu.Iva_purchase,
                Purchase_Remarks = pu.Purchase_Remarks
            }).OrderBy(pu => pu.Id).ToList());
        }
        public void AddPurchaseToList(PurchaseRequest purchase)
        {
            this.myPurchases.Add(purchase);
            RefreshPurchasesList();
        }

        public void UpdatePurchaseInList(PurchaseRequest purchase)
        {
            var previousPurchase = myPurchases.Where(pu => pu.Id == purchase.Id).FirstOrDefault();

            if (previousPurchase != null)
            {
                this.myPurchases.Remove(previousPurchase);
            }
            this.myPurchases.Add(purchase);
            RefreshPurchasesList();
        }
        public void DeletePurchaseInList(int purchaseId)
        {
            var previousPurchase = myPurchases.Where(pu => pu.Id == purchaseId).FirstOrDefault();

            if (previousPurchase != null)
            {
                this.myPurchases.Remove(previousPurchase);
            }
            RefreshPurchasesList();
        }
    
    }
}
