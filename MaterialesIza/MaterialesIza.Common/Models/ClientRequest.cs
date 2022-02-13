using System.Collections.Generic;

namespace MaterialesIza.Common.Models
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<OrderRequest> Orders { get; set; }
    }
}
