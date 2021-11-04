using System.ComponentModel.DataAnnotations;

namespace MaterialesIza.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MaxLength(10)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
