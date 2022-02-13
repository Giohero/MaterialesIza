using System;

namespace MaterialesIza.Common.Models
{
    public class OrderDetailsResponse
    {
        public int Id { get; set; }

        public DateTime Date_Order { get; set; }

        public double Total_Order { get; set; }

        public string Service { get; set; }

        public User User { get; set; }
    }
}
