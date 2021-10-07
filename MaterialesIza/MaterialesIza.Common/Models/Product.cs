using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MaterialesIza.Common.Models
{
    public  class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("productTypes")]
        public object ProductTypes { get; set; }

        [JsonPropertyName("saleDetails")]
        public List<SaleDetails> SaleDetails { get; set; }

        [JsonPropertyName("purchaseDetails")]
        public object PurchaseDetails { get; set; }



    }
}
