using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.OpenApi.Extensions;
using WebAppPayments.Models;
using WebAppPayments.Models.DTO;
using WebAppPayments.Models.Enumerations;
using WebAppPayments.Models.Response;
using WebAppPayments.Models.Request;

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

        public IQueryable<PaymentResult> GetPayments()
        {
            var paymentRecords = _dbContext.Payments
                .Include(c => c.Client)
                .Select(pt => new PaymentResult
                {
                    PaymentId = pt.PaymentId,
                    Client = pt.Client,
                    PaymentTypeName = pt.PaymentTypeId.GetDisplayName(),
                    PaymentDate =pt.PaymentDate,
                    PaymentDescription =pt.PaymentDescription,
                    PaymentAmount = pt.PaymentAmount
                });
                
           return paymentRecords;
        }

        public bool CreatePayments(Payment model)
        {
            bool isCreate = false;
            try
            {
                Payment payment = new Payment();
                
                payment.PaymentTypeId = model.PaymentTypeId;
                
                payment.Client =_dbContext.Clients.FirstOrDefault(c=>c.Name==model.Client.Name && c.LastName==model.Client.LastName )?? new Client()
                {
                    ClientId = model.Client.ClientId,
                    Name = model.Client.Name,
                    LastName = model.Client.LastName
                };
                
                payment.PaymentDate = model.PaymentDate;
                payment.PaymentDescription = model.PaymentDescription;
                payment.PaymentAmount = model.PaymentAmount;
                _dbContext.Payments.Add(payment);
                _dbContext.SaveChanges();

                isCreate = true;
            }
            catch
            {
                isCreate = false;
            }

            return isCreate;
        }

        public bool UpdatePayments(Payment model)
        {
            bool isUpdate = false;
            try
            {
                Payment payment = _dbContext.Payments.Find(model.PaymentId);
                
                payment.Client =_dbContext.Clients.FirstOrDefault(c=>c.Name==model.Client.Name && c.LastName==model.Client.LastName )?? new Client()
                {
                    ClientId = model.Client.ClientId,
                    Name = model.Client.Name,
                    LastName = model.Client.LastName
                };
                payment.PaymentDate = model.PaymentDate;
                payment.PaymentDescription = model.PaymentDescription;
                payment.PaymentAmount = model.PaymentAmount;
                payment.PaymentTypeId = model.PaymentTypeId;
                
                
                
                _dbContext.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                
                isUpdate = true;
            }
            catch
            {
                isUpdate = false;
            }

            return isUpdate;


        }

        public bool DeletePayments(int PaymentId)
        {
            bool isDelete = false;
            try
            {
               
                Payment payment =_dbContext.Payments.Find(PaymentId);
                 _dbContext.Remove(payment);
                 _dbContext.SaveChanges();
                 
            }catch
            {
                isDelete = false;
            }

            return isDelete;
        }
    }
}