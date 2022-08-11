using Microsoft.AspNetCore.Mvc;
using WebAppPayments.Models.Response;
using WebAppPayments.Models.DTO;
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
    public ActionResult<Response> Read(string clientName, string paymentType,int page,  int pageSize = 10)
    {
        var response = new Response();
        try
        {
            var paymentsList = _paymentService.GetPayments(page,pageSize,clientName,paymentType).ToList();
            
            response.ResponseCode = "Success";
            response.Message = "Ok";
            response.Payments = paymentsList;
        }
        catch(Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        return response;
    }
    [HttpPost]
    public ActionResult<ResponseCreate> Create(PaymentDTOCreate  paymentDto)
    {
        var response = new ResponseCreate();
        try
        {
            var savedPaymentId= _paymentService.CreatePayments(paymentDto);
            if (savedPaymentId > 0)
            {
                response.ResponseCode = "Success";
                response.Message = "Ok";
                response.SavedPaymentId = savedPaymentId;
            }
            else
            {
                response.Message = "Something went wrong";
                response.SavedPaymentId = 0;
                response.ResponseCode = StatusCodes.Status500InternalServerError.ToString();
            }
        }
        catch (Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        return response;
    }
    [HttpGet("{id:int}")]
    public ActionResult<Response> Details(int id)
    {
        var response = new Response();
        try
        {
            var paymentsList = _paymentService.GetDetails(id);
            
            response.ResponseCode = "Success";
            response.Message = "Ok";
            response.Payments = paymentsList;
        }
        catch(Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        return response;
    }
    [HttpDelete("{paymentId}")]
    public ActionResult<Response> Delete(int paymentId)
    {
        var response = new Response();
        try
        {
            _paymentService.DeletePayments(paymentId);
            response.ResponseCode = "Success";
            response.Message = "Ok";
        }
        catch (Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        return response;
    }
    
    [HttpPut]
    public ActionResult<Response> Update(PaymentDTOUpdate paymentDto)
    {
        var response = new Response();
        try
        {
            var updatePayment=_paymentService.UpdatePayments(paymentDto);

            if (updatePayment == true)
            {
                response.ResponseCode = "Success";
                response.Message = "Ok";
            }
            else
            {
                response.Message = "Something went wrong";
                response.ResponseCode = StatusCodes.Status500InternalServerError.ToString();
            }
            
        }
        catch (Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        return response;
    }
    [HttpPut("UpdatePay")]
    public ActionResult<Response> UpdatePaymentId(PaymentDTOPay paymentDtoPay)
    {
        var response = new Response();
        try
        {
            _paymentService.UpdatePaymentId(paymentDtoPay);
            response.ResponseCode = "Success";
            response.Message = "Ok";
        }
        catch(Exception ex)
        {
            response.Message =ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        return response;

    }



}