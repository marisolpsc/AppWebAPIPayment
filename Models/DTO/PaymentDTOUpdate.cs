namespace WebAppPayments.Models.DTO;

public class PaymentDTOUpdate
{
    public int PaymentId { get; set; }
    public string? ClientKey { get; set; }
    public string? PaymentType { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public int PaymentAmount { get; set; }
    

  
}