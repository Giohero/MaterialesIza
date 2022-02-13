using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleRequest
    {
        public int Id { get; set; }

        public ClientRequest Client { get; set; }

        public EmployeeRequest Employee { get; set; }

        public ICollection<SaleDetailsRequest> SaleDetails { get; set; }
    }
}
