using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class OrderRequest
    {
        public int Id { get; set; }

        public DateTime Date_Order { get; set; }

        public double Total_Order { get; set; }

        public double Iva_Order { get; set; }

        public string Order_Remarks { get; set; }

        public EmployeeRequest Employee { get; set; }

        public ICollection<OrderDetailsRequest> OrderDetails { get; set; }
    }
}
