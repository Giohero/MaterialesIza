using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Service
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Id} {this.Name}";
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("serviceType")]
        public object ServiceType { get; set; }

        [JsonProperty("orderDetails")]
        public object OrderDetails { get; set; }

    }
}
