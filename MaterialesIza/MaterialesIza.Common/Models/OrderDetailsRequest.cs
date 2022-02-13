using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class OrderDetailsRequest
    {
        public int Id { get; set; }

        public DateTime Date_Order { get; set; }

        public double Total_Order { get; set; }

        public double Iva_Order { get; set; }

        public string Order_Remarks { get; set; }

        public ServiceRequest Service { get; set; }
    }
}
