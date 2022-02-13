using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class ProductRequest
    {
        public int Id { get; set; }
  
        public string Name { get; set; }
 
        public int Price { get; set; }
  
        public int Quantity { get; set; }
      
        public string Description { get; set; }

        public string ProductTypes { get; set; }
    }
}
