using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseDetailsRequest
    {
        public int Id { get; set; }

        public DateTime Date_Purchase { get; set; }

        public double Total_Purchase { get; set; }

        public double Iva_Purchase { get; set; }

        public string Purchase_Remarks { get; set; }

        
    }
}
