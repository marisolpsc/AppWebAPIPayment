using WebAppPayments.Models;
using WebAppPayments.Models.Request;

namespace WebAppPayments.Services
{
    public interface IPaymentService
    {
        IQueryable<Payment> GetPayments();
        bool CreatePayments(PaymentRequest model);
        bool UpdatePayments(PaymentRequest model);
        bool DeletePayments(int PaymentId);
        

    }
}
