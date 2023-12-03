using FitHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public IActionResult PaymentForm()
        {
            // Declare and return an empty payment method form at the beginning
            var paymentMethod = new PaymentMethod();
            return View(paymentMethod);
        }

        [HttpPost]
        /*[Route("/api/payment/process")]*/
        public IActionResult ProcessPayment(PaymentMethod paymentMethod)
        {
            // Always set the payment to success for this project
            if (ModelState.IsValid)
            {
                return Ok(new { Success = true, Message = "Payment Successful !!!" });
            }
            
            return View("PaymentForm", paymentMethod);
        }
    }
}
