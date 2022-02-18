using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialesIza.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Name")]

        public string Name { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Price")]

        public int Price { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Quantity")]
       
        public string Description { get; set; }

       
        public ProductType ProductTypes { get; set; }


        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }

    }
    
}
