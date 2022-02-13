using System.ComponentModel.DataAnnotations;

namespace MaterialesIza.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
