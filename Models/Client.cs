using System;
using System.Collections.Generic;

namespace WebAppPayments.Models
{
    public partial class Client
    {
        /*public Client()
        {
            Payments = new HashSet<Payment>();
        }*/

        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        //public virtual ICollection<Payment> Payments { get; set; }
    }
}
