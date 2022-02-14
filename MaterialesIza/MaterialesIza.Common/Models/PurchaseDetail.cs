using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date_Purchase")]
        public DateTime DatePurchase { get; set; }

        [JsonProperty("total_Purchase")]
        public int TotalPurchase { get; set; }

        [JsonProperty("iva_Purchase")]
        public int IvaPurchase { get; set; }

        [JsonProperty("purchase_Remarks")]
        public string PurchaseRemarks { get; set; }

        //[JsonProperty("product")]
        //public Product Product { get; set; }

        public override string ToString()
        {
            return $"{this.Id} {this.DatePurchase} {this.TotalPurchase} {this.PurchaseRemarks} ";
        }
    }
}
