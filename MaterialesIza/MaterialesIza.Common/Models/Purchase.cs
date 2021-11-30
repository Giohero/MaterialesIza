﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Purchase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("provider")]
        public Provider Provider { get; set; }

        //public override string ToString()
        //{
        //    return $"{this.Id} {this.Provider}";
        //}

        [JsonProperty("purchaseDetails")]
        public List<object> PurchaseDetails { get; set; }
    }
}
