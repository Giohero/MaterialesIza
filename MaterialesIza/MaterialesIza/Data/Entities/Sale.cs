using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Sale:IEntity
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        public Employee Employee { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

    }
}
