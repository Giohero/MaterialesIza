using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class PurchaseDetail : IEntity
    {
        public int Id { get; set; }

        public DateTime Date_purchase { get; set; }

        public double Total_purchase { get; set; }

        public double Iva_purchase { get; set; }

        public string Purchase_Remarks { get; set; }

        // public Product Product { get; set; }

        public Purchase Purchase { get; set; }

    }
    
}
