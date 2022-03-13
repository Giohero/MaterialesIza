using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class PurchaseTypeRequest
    {
        public int Id { get; set; }

        public string PurchaseName { get; set; }

        public DateTime Date_purchase { get; set; }

        public string Purchase_Remarks { get; set; }


        public string PurchasesTypes { get; set; }

    }
}
