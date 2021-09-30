using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public Service Service { get; set; }
    }        
}
