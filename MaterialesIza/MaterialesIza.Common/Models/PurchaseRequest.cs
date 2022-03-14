using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseRequest
    {
        public int Id { get; set; }

        public string PurchaseName { get; set; } //Eliminar o dejar


        public DateTime Date_purchase { get; set; }

        public double Total_purchase { get; set; }

        public double Iva_purchase { get; set; }

        public string Purchase_Remarks { get; set; }

        public EmployeeRequest Employee { get; set; }

        public ProviderRequest Provider { get; set; }

        public ICollection<PurchaseDetailsRequest> PurchaseDetails { get; set; }
    }
}
