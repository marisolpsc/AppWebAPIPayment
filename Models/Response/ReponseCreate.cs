namespace WebAppPayments.Models.Response;

public class ResponseCreate
{
    public string ResponseCode { get; set; }
    public string Message { get; set; }
    
    public int? SavedPaymentId { get; set; }

}