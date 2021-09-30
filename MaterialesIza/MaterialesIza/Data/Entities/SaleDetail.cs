using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class SaleDetail:IEntity
    {
        public int Id { get; set; }

        public Sale Sale { get; set; }

        public Product Product { get; set; }

        
    }
}
