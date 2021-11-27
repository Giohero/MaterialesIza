﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public User User{ get; set; }

        [JsonProperty("orders")]
        public object Orders { get; set; }

        [JsonProperty("sales")]
        public object Sales { get; set; }
    }
}