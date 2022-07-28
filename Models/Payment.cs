using System;
using System.Collections.Generic;

namespace WebAppPayments.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int ClientId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentDescription { get; set; }
        public int? PaymentAmount { get; set; }
        public int PaymentTypeId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
    }
}
