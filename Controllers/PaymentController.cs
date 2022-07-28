using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPayments.Models.Response;
using WebAppPayments.Models;
using WebAppPayments.Models.Request;
using System.Data.Entity;
using System.Globalization;

namespace WebAppPayments.Controllers;



[Route("api/[controller]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly PaymentsContextContext _dbContext;

    public PaymentController(PaymentsContextContext context)
    {
        _dbContext = context;
    }
    [HttpGet]
    public IActionResult Read()
    {
        Response response = new Response();
        try
        {

                var paymentsRecords = 
                from p in _dbContext.Payments  
                join c in _dbContext.Clients on p.ClientId equals c.ClientId into table1  
                from c in table1.DefaultIfEmpty()
                join pt in _dbContext.PaymentTypes on p.PaymentTypeId equals pt.PaymentTypeId into table2  
                from i in table2.DefaultIfEmpty() 
                select new   
                {
                    p.PaymentId,
                    p.Client,
                    p.PaymentType,
                    p.PaymentDate,
                    p.PaymentDescription,
                    p.PaymentAmount
                };
             
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
                response.Payments = paymentsRecords.ToList();
                    

         
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
            response.ResponseCode = "Fail";
        }
       
        
        return Ok(response);
    }
    [HttpPost]
    public IActionResult Create(PaymentRequest model)
    {
        Response response = new Response();
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
                
                
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
                 
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
            response.ResponseCode = "Fail";
        }
       
        
        return Ok(response);
    }
    [HttpPut]
    public IActionResult Update(PaymentRequest model)
    {
        Response response = new Response();
        try
        {
                Payment payment =_dbContext.Payments.Find(model.PaymentId);
              
                payment.ClientId = model.ClientId;
                payment.PaymentTypeId = model.PaymentTypeId;
                payment.PaymentDate = model.PaymentDate;
                payment.PaymentDescription = model.PaymentDescription;
                payment.PaymentAmount = model.PaymentAmount;
                _dbContext.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                
                
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
                    
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
            response.ResponseCode = "Fail";
        }
       
        
        return Ok(response);
    }
    [HttpDelete("{PaymentId}")]
    public IActionResult Delete(int PaymentId)
    {
        Response response = new Response();
        try
        {
                Payment payment =_dbContext.Payments.Find(PaymentId);
                _dbContext.Remove(payment);
                _dbContext.SaveChanges();
                
                
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
                
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
            response.ResponseCode = "Fail";
        }
       
        
        return Ok(response);
    }

  
}