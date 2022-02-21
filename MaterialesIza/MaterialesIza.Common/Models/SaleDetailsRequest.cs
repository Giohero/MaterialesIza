using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class SaleDetailsRequest
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

       // public SaleRequest Sale { get; set; }

        public ProductRequest Product { get; set; }
    }
}
