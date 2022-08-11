using System;
using System.Collections.Generic;
using  WebAppPayments.Models.Enumerations;

namespace WebAppPayments.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentDescription { get; set; }
        public int? PaymentAmount { get; set; }
        public PaymentType? PaymentTypeId { get; set; }
        public virtual Client Client { get; set; } = null!;

    }
}
