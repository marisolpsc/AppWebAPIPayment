using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppPayments.Models
{
    public partial class Client
    {
        [JsonIgnore]
        public int ClientId { get; set; }
        public string Key { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }

       
    }
}
