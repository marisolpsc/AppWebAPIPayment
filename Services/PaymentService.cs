using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using WebAppPayments.Models;
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

        public IQueryable<Payment>  GetPayments()
        {


            var paymentsRecords = 
                from p in _dbContext.Payments  
                join c in _dbContext.Clients on p.ClientId equals c.ClientId into table1  
                from c in table1.DefaultIfEmpty()
                join pt in _dbContext.PaymentTypes on p.PaymentTypeId equals pt.PaymentTypeId into table2  
                from i in table2.DefaultIfEmpty() 
                select new Payment() 
                { 
                    PaymentId = p.PaymentId,
                    Client = new Client()
                    {
                        Name=c.Name.TrimEnd(),
                        LastName= c.LastName.TrimEnd()
                    },
                    PaymentType = new PaymentType()
                    {
                        PaymentType1=GetClients(i.PaymentTypeId)
                    },
                    PaymentDate = p.PaymentDate,
                    PaymentDescription = p.PaymentDescription.TrimEnd(),
                    PaymentAmount = p.PaymentAmount
               
                };
            return paymentsRecords;
        }

        public bool CreatePayments(PaymentRequest model)
        {
            bool isCreate = false;
            try
            {
                Payment payment = new Payment();

                payment.ClientId = model.ClientId;
                payment.PaymentTypeId = model.PaymentTypeId;
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

        public bool UpdatePayments(PaymentRequest model)
        {
            bool isUpdate = false;
            try
            {
                Payment payment = _dbContext.Payments.Find(model.PaymentId);
                payment.ClientId = model.ClientId;
                payment.PaymentTypeId = model.PaymentTypeId;
                payment.PaymentDate = model.PaymentDate;
                payment.PaymentDescription = model.PaymentDescription;
                payment.PaymentAmount = model.PaymentAmount;
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

        public static string GetClients(int PaymentTypeId)
        {
            switch (PaymentTypeId)
            {
                case 0:
                    return "Unknown";
                case 1:
                    return "CreditCard";
                case 2:
                    return "DebitCard";
                case 3:
                    return "Paypal";
                default:
                    return "Invalid Data for Payment Type";
                
            }
            
        }

    }
}