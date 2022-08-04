using System.Text.Json.Serialization;
using WebAppPayments.Models.Enumerations;

namespace WebAppPayments.Models.Request;

public class PaymentRequestGet
{
    [JsonIgnore]
    public int PaymentId { get; set; }
    public int ClientId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public int? PaymentAmount { get; set; }
    
    public int PaymentTypeId { get; set; }
    public virtual Client Client { get; set; }
    
}