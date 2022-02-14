using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        //[JsonProperty("client")]
        //public Client Client { get; set; }

        [JsonProperty("orderDetails")]
        public object OrderDetails { get; set; }
    }
}
