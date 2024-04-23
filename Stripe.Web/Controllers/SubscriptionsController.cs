using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Web.Models;
using Stripe.Web.Models.Subscriptions;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly IPaymentsGateway _paymentsGateway;
        private readonly IConfiguration _configuration;

        public SubscriptionsController(IPaymentsGateway paymentsGateway,
            IConfiguration configuration)
        {
            _paymentsGateway = paymentsGateway;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["StripePulbicKey"] = _configuration.GetSection("Stripe").GetValue<string>("publicKey");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIntent(string email)
        {
            var customer = await this._paymentsGateway.GetCustomerByEmail(email);

            var options = new SetupIntentCreateOptions
            {
                Customer = customer.Id,
            };
            var service = new SetupIntentService();
            var intent = service.Create(options);
            ViewData["ClientSecret"] = intent.ClientSecret;

            var plans = await this._paymentsGateway.GetPlans();
            var paymentMethods = await this._paymentsGateway.GetPaymentMethods(customer.Id, PaymentMethodType.Card);

            return View(new CreateSubscriptionIntentModel(customer, plans, paymentMethods));
        }

        public async Task<ActionResult<Subscription>> Create(CreateSubscriptionRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.CustomerId,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethodId, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.CustomerId, customerOptions);

            // Create subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = req.CustomerId,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = req.PriceId,
                    },
                },
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = subscriptionService.Create(subscriptionOptions);
                return View(subscription);
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                return BadRequest();
            }

        }
    }
}
