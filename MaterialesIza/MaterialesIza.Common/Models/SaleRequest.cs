using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleRequest
    {
        public int Id { get; set; }

        public DateTime Date_Sale { get; set; }

        public double Total_Sale { get; set; }

        public double Iva_Sale { get; set; }

        public string Sales_Remarks { get; set; }

        public ClientRequest Client { get; set; }

        //public ICollection<SaleDetailsRequest> SaleDetails { get; set; }
    }
}
