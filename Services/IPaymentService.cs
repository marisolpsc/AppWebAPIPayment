using WebAppPayments.Models;
using WebAppPayments.Models.DTO;
using WebAppPayments.Models.Request;

namespace WebAppPayments.Services
{
    public interface IPaymentService
    {
        IQueryable<PaymentResult> GetPayments();
        bool CreatePayments(Payment model);
        bool UpdatePayments(Payment model);
        bool DeletePayments(int PaymentId);
        

    }
}
