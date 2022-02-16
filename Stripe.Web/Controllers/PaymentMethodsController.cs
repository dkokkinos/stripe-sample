using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentsGateway _paymentsGateway;
        private readonly IConfiguration _configuration;

        public PaymentMethodsController(IPaymentsGateway paymentsGateway,
            IConfiguration configuration)
        {
            _paymentsGateway = paymentsGateway;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string customerEmail)
        {
            var customer = await _paymentsGateway.GetCustomerByEmail(customerEmail, PaymentModelInclude.PaymentMethods);
            if (customer == null)
                return View();
            ViewData["StripePublicKey"] = _configuration.GetSection("Stripe").GetValue<string>("publicKey");
            ViewData["ClientSecret"] = (await _paymentsGateway.PrepareForFuturePayment(customer.Id)).IntentSecret;
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string customerEmail, string paymentMethodId)
        {
            await this._paymentsGateway.DeletePaymentMethod(paymentMethodId);
            return RedirectToAction("Index", new { customerEmail = customerEmail });
        }
    }
}
