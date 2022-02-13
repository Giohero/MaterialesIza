using System.Collections.Generic;

namespace MaterialesIza.Data.Entities
{
    public class Employee:IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Sale> Sales { get; set; }

        //public ICollection<Order> Orders { get; set; }
    }
}
