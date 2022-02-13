using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleDetailsRequest
    {
        public int Id { get; set; }

        public DateTime Date_Sale { get; set; }

        public double Total_Sale { get; set; }

        public double Iva_Sale { get; set; }

        public string Sales_Remarks { get; set; }

        public ProductRequest product { get; set; }
    }
}
