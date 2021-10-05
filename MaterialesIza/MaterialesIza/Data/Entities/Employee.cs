using System.Collections.Generic;

namespace MaterialesIza.Data.Entities
{
    public class Employee:IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        //public Sale Sale { get; set; }

        //public Order Order { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
