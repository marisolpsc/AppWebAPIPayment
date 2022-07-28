namespace WebAppPayments.Models.Request;

public class PaymentRequest
{
    public int PaymentId { get; set; }
    public int ClientId { get; set; }
    public int PaymentTypeId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public int? PaymentAmount { get; set; }
    
}