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
        public DateTime DateOrder { get; set; }

        [JsonProperty("total_Sale")]
        public int TotalOrder { get; set; }

        [JsonProperty("iva_Sale")]
        public int IvaOrder { get; set; }

        [JsonProperty("sales_Remarks")]
        public string OrderRemarks { get; set; }

        //[JsonProperty("service")]
        //public Service Service { get; set; }
        

    }
}
