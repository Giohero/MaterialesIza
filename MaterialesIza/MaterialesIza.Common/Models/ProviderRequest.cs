using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialesIza.Common.Models
{
    public class ProviderRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName => $"{LastName} {FirstName}";

        public ICollection<PurchaseRequest> Purchases { get; set; }
    }
}
