using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Admin
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }
    }
}
