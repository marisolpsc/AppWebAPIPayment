using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebAppPayments.Models.DTO;

public class PaymentDTOCreate
{

    public string? ClientKey { get; set; }
    public string? PaymentType { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public int PaymentAmount { get; set; }
    

  
}