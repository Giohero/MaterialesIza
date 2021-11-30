using System.Collections.Generic;

namespace MaterialesIza.Data.Entities
{
    public class Provider : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        //public ICollection<Purchase> Purchases { get; set; }


    }
}
