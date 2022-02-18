using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class OrderDetailsRequest
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public OrderRequest Order { get; set; }

        public ServiceRequest Service { get; set; }
    }
}
