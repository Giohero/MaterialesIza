using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class OrderRequest
    {
        public int Id { get; set; }

        public ClientRequest Client { get; set; }

        public EmployeeRequest Employee { get; set; }

        public ICollection<OrderDetailsRequest> OrderDetails { get; set; }
    }
}
