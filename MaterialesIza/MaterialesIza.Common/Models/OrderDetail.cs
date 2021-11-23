using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class OrderDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date_Sale")]
        public DateTime DateSale { get; set; }

        [JsonProperty("total_Sale")]
        public int TotalSale { get; set; }

        [JsonProperty("iva_Sale")]
        public int IvaSale { get; set; }

        [JsonProperty("sales_Remarks")]
        public object SalesRemarks { get; set; }



        [JsonProperty("order")]
        public object Order { get; set; }

        [JsonProperty("service")]
        public object Service { get; set; }
    }
}
