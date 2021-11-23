using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sale")]
        public object Sale { get; set; }

        
    }
}
