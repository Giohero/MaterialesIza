using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class ServiceType : IEntity
    {
        public int Id { get; set; }
        public string TypeService { get; set; }

        public ICollection<Service> Services { get; set; }
    }
   
}
