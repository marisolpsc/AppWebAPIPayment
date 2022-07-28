using System;
using System.Collections.Generic;

namespace WebAppPayments.Models
{
    public partial class PaymentType
    {
        /*public PaymentType()
        {
            Payments = new HashSet<Payment>();
        }*/

        public int PaymentTypeId { get; set; }
        public string? PaymentType1 { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
