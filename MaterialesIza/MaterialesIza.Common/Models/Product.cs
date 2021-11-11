using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productTypes")]
        public object ProductTypes { get; set; }

        [JsonProperty("saleDetails")]
        public object SaleDetails { get; set; }

        [JsonProperty("purchaseDetails")]
        public object PurchaseDetails { get; set; }
    
    }
}
