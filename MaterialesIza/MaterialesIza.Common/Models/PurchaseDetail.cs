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

        [JsonProperty("date_Sale")]
        public string DateSale { get; set; }

        [JsonProperty("total_Sale")]
        public int TotalSale { get; set; }

        [JsonProperty("iva_Sale")]
        public int IvaSale { get; set; }

        [JsonProperty("sales_Remarks")]
        public string SalesRemarks { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }
    }
}
