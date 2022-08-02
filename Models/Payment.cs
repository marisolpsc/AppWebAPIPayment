using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebAppPayments.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public virtual Client Client { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
        public DateTime? PaymentDate { get; set; }
        public string? PaymentDescription { get; set; }
        public int? PaymentAmount { get; set; }
        
        [IgnoreDataMember]
        [JsonIgnore]
        public int ClientId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public int PaymentTypeId { get; set; }

        
       
    }
}
