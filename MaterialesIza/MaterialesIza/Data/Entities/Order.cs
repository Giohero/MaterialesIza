﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public Client Client { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
   
    }
     
}
