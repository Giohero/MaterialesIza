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
        public User User { get; set; }

        public override string ToString()
        {
            return $"{this.Id} {this.User}";
        }
    }
}
