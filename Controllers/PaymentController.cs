using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPayments.Models.Response;
using WebAppPayments.Models;
using WebAppPayments.Models.Request;
using System.Data.Entity;
using System.Globalization;
using WebAppPayments.Services;

namespace WebAppPayments.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
    

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet]
    public IActionResult Read()
    {
        Response response = new Response();
        try
        {
            var paymentsList = _paymentService.GetPayments().ToList();
            
            response.ResponseCode = "Sucess";
            response.Message = "Ok";
            response.Payments = paymentsList;
            
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
            var isCreate=_paymentService.CreatePayments(model);
            var paymentsList = _paymentService.GetPayments().ToList();
            if (isCreate == true)
            {
                response.Payments = paymentsList;
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
            }
        }
        catch (Exception ex)
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
            var isCreate=_paymentService.UpdatePayments(model);
            var paymentsList = _paymentService.GetPayments().ToList();
                 
            if (isCreate == true)
            {
                response.Payments = paymentsList;
                response.ResponseCode = "Sucess"; 
                response.Message = "Ok";
            }
        }
        catch (Exception ex)
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
            var isDelete=_paymentService.DeletePayments(PaymentId);
            var paymentsList = _paymentService.GetPayments().ToList();
            
            if (isDelete == true)
            {
                response.Payments = paymentsList;
                response.ResponseCode = "Sucess";
                response.Message = "Ok";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.ResponseCode = "Fail";
        }
        return Ok(response);
    }

  
}