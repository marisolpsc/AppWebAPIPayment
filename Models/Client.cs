using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebAppPayments.Models
{
    [DataContract]
    public partial class Client
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

       
    }
}
