using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Sale:IEntity
    {
        public int Id { get; set; }

        public DateTime Date_Sale { get; set; }

        public double Total_Sale { get; set; }

        public double Iva_Sale { get; set; }

        public string Sales_Remarks { get; set; }

        public Client Client { get; set; }

        public Employee Employee { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

    }
}
