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
        public User Employee { get; set; }

        //public override string ToString()
        //{
        //    return $"{this.Id} {this.Employee}";
        //}

        [JsonProperty("client")]
        public User Client { get; set; }

        [JsonProperty("orderDetails")]
        public object OrderDetails { get; set; }
    }
}
