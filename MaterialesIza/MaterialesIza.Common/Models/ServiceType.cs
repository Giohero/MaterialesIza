using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class ServiceType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("typeService")]
        public string TypeService { get; set; }

        [JsonProperty("services")]
        public object Services { get; set; }
    }
}
