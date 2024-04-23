using Stripe.Web.Services;
using System.Collections.Generic;

namespace Stripe.Web.Models.Subscriptions
{
    public class CreateSubscriptionIntentModel
    {
        public CustomerDto Customer { get; }
        public List<PlanDto> Plans { get; }
        public List<PaymentMethodModel> PaymentMethods { get; }

        public CreateSubscriptionIntentModel(CustomerDto customer, List<PlanDto> plans, List<PaymentMethodModel> paymentMethods)
        {
            Customer = customer;
            Plans = plans;
            PaymentMethods = paymentMethods;
        }
    }
}
