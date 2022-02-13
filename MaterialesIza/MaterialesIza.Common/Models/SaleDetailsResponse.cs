using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleDetailsResponse
    {
        public int Id { get; set; }

        public DateTime Date_Sale { get; set; }

        public double Total_Sale { get; set; }

        public string Name { get; set; }

        public User User { get; set; }
    }
}
