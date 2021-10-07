using Newtonsoft.Json;

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
