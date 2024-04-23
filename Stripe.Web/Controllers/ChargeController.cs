using Microsoft.AspNetCore.Mvc;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Controllers
{
    public class ChargeController : Controller
    {
        private readonly IPaymentsGateway _paymentsGateway;

        public ChargeController(IPaymentsGateway paymentsGateway)
        {
            _paymentsGateway = paymentsGateway;
        }

        public async Task<IActionResult> Index(string customerEmail)
        {
            if (string.IsNullOrEmpty(customerEmail))
                return View();
            var customer = await _paymentsGateway.GetCustomerByEmail(customerEmail, PaymentIncludeDto.PaymentMethods);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string customerEmail, string paymentMethodId, decimal amount)
        {
            await _paymentsGateway.ChargeWithCustomerEmail(customerEmail, paymentMethodId, Currency.Eur, (long)amount * 100);
            return RedirectToAction("Index", new { customerEmail });
        }
    }
}
