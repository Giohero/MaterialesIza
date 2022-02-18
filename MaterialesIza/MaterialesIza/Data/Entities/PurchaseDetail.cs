using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class PurchaseDetail : IEntity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Purchase Purchase { get; set; }

    }
    
}
