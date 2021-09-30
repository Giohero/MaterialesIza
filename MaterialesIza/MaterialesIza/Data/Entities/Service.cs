using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Service:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
