using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseRequest
    {
        public int Id { get; set; }

        public EmployeeRequest Employee { get; set; }

        public ProviderRequest Provider { get; set; }

        public ICollection<PurchaseDetailsRequest> PurchaseDetails { get; set; }
    }
}
