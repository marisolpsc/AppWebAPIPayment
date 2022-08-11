using WebAppPayments.Models;
using WebAppPayments.Models.DTO;

namespace WebAppPayments.Services
{
    public interface IPaymentService
    {
        IQueryable<PaymentDTO> GetPayments(int page, int pageSize,string clientName,string paymentType);
        int CreatePayments(PaymentDTOCreate paymentDto);
        bool UpdatePayments(PaymentDTOUpdate model);
        void DeletePayments(int paymentId);
        void UpdatePaymentId(PaymentDTOPay paymentDtoPay);
        IQueryable<PaymentDTO> GetDetails(int id);
    }
}
