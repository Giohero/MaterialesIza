using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class ProductType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products  { get; set; }

    }
    
   
}
