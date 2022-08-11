using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.OpenApi.Extensions;
using WebAppPayments.Models;
using WebAppPayments.Models.DTO;
using WebAppPayments.Models.Enumerations;
using WebAppPayments.Models.Response;

namespace WebAppPayments.Services
{
    [Export(typeof(IPaymentService))]
    internal class PaymentService : IPaymentService
    {
        private readonly PaymentsContext _dbContext;
        
        public PaymentService(PaymentsContext context)
        {
            _dbContext = context;
        }
        public IQueryable<PaymentDTO> GetPayments(int page, int pageSize,string clientName, string paymentType)
        {
            var paymentRecords = _dbContext.Payments
                .Include(c => c!.Client)
                .Where(p=>p.Client.Name==clientName && p.PaymentTypeId== (PaymentType)Enum.Parse(typeof(PaymentType),paymentType))
                .Select(pt => new PaymentDTO()
                {
                    PaymentId = pt!.PaymentId,
                    Client = pt.Client,
                    PaymentTypeName = pt.PaymentTypeId.GetDisplayName(),
                    PaymentDate =pt.PaymentDate,
                    PaymentDescription =pt.PaymentDescription,
                    PaymentAmount = pt.PaymentAmount
                }).Skip((page-1)*pageSize).Take(pageSize);
            
           return paymentRecords;
        }

        public int CreatePayments(PaymentDTOCreate paymentDto)
        {
            var savedPaymentId = 0;
            try
            {
                var clientId = _dbContext.Clients.Where(c => c.Key == paymentDto.ClientKey).Select(c => c.ClientId)
                    .FirstOrDefault();
                
                var payment = new Payment();
                if (clientId>0)
                {
                    payment.ClientId =clientId;
                    if (paymentDto.PaymentType != null)
                        payment.PaymentTypeId = (PaymentType?)Enum.Parse(typeof(PaymentType), paymentDto.PaymentType);
                    payment.PaymentDate = paymentDto.PaymentDate;
                    payment.PaymentDescription = paymentDto.PaymentDescription;
                    payment.PaymentAmount = paymentDto.PaymentAmount;
                    
                    _dbContext.Payments.Add(payment);
                    _dbContext.SaveChanges();

                    savedPaymentId = payment.PaymentId;
                }

                return savedPaymentId;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return savedPaymentId;
        }
        public IQueryable<PaymentDTO> GetDetails(int id)
        {
            var paymentRecords = _dbContext.Payments
                .Include(c => c!.Client)
                .Where(p=>p.PaymentId==id)
                .Select(pt => new PaymentDTO()
                {
                    PaymentId = pt!.PaymentId,
                    Client = pt.Client,
                    PaymentTypeName = pt.PaymentTypeId.GetDisplayName(),
                    PaymentDate =pt.PaymentDate,
                    PaymentDescription =pt.PaymentDescription,
                    PaymentAmount = pt.PaymentAmount
                });
            
            return paymentRecords;
        }
        
        public void DeletePayments(int paymentId)
        {
            var payment = _dbContext.Payments.Find(paymentId);
            try
            {
                _dbContext.Remove(payment);
                _dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool UpdatePayments(PaymentDTOUpdate paymentDto)
        {
            var saved = false;
            try
            {
                Payment? payment = _dbContext.Payments.Find(paymentDto.PaymentId);
                var clientId = _dbContext.Clients.Where(c => c.Key == paymentDto.ClientKey).Select(c => c.ClientId)
                    .FirstOrDefault();

                if (payment != null && clientId>0)
                {
                    payment.ClientId = clientId;
                    payment.PaymentTypeId = (PaymentType)Enum.Parse(typeof(PaymentType),paymentDto.PaymentType);
                    payment.PaymentDate = paymentDto.PaymentDate;
                    payment.PaymentDescription = paymentDto.PaymentDescription;
                    payment.PaymentAmount = paymentDto.PaymentAmount;
                    
                    _dbContext.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                _dbContext.SaveChanges();
                saved = true;


            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                saved = false;

            }

            return saved;

        }

        public void UpdatePaymentId(PaymentDTOPay paymentDtoPay)
        {
            try
            {
                Payment? payment = _dbContext.Payments.Find(paymentDtoPay.PaymentId);
                if (payment != null)
                {
                    payment.PaymentDate = paymentDtoPay.PaymentDate;
                    
                    _dbContext.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                _dbContext.SaveChanges();

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}