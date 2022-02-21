using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseDetailsRequest
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ProductRequest Product { get; set; }

    }
}
