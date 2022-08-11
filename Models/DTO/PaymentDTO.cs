using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebAppPayments.Models.DTO;

public class PaymentDTO
{

    public int PaymentId { get; set; }
    public virtual Client Client { get; set; } = null!;
    public string? PaymentTypeName { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public int? PaymentAmount { get; set; }
  
}