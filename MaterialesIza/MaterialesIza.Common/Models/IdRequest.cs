using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class IdRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
