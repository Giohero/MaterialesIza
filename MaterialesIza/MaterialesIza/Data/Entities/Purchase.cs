using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Purchase : IEntity
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        //public Provider Provider { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
    
    
}
