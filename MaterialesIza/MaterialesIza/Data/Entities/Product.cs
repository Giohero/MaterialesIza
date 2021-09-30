using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }


        public ProductType ProductType { get; set; }


        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }

    }
    
}
