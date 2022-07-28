namespace WebAppPayments.Models.Response;

public class Response
{
    public string ResponseCode { get; set; }
    public string Message { get; set; }
    public object Payments { get; set; }

    public Response()
    {

        this.ResponseCode = "Success";
    }
}