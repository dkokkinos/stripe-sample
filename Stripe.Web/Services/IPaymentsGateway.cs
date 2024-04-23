using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Services
{
    public interface IPaymentsGateway
    {
        Task<List<PlanDto>> PopulatePlans(List<PlanDto> plans);
        Task<List<PlanDto>> GetPlans();

        Task<bool> CreateCustomer(string name, string email, string systemId);
        Task<List<CustomerDto>> GetCustomers(int take);
        Task<CustomerDto> GetCustomerByEmail(string email, params PaymentIncludeDto[] include);
        Task<CustomerDto> DeleteCustomerByEmail(string email);

        /// <summary>
        /// when you want to add a payment method for future payment for this particular customer.
        /// Use the return object depending depending the payment provider, e.g. for stripe use the IntentSecret as the ClientSecret
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<FuturePaymentIntent> PrepareForFuturePayment(string customerId);
        Task<FuturePaymentIntent> PrepareForFuturePaymentWithCustomerEmail(string customerEmail);

        Task<List<PaymentMethodModel>> GetPaymentMethods(string customerId, PaymentMethodType paymentMethodType);
        Task<List<PaymentMethodModel>> GetPaymentMethodsByCustomerEmail(string customerEmail, PaymentMethodType paymentMethodType);

        Task<PaymentMethodModel> AttachPaymentMethod(string paymentMethodId, string customerId, bool makeDefault);
        Task DeletePaymentMethod(string paymentMethodId);

        Task<bool> CreateSubscription(string customerEmail, string priceId);

        Task Charge(string customerId, string paymentMethodId, Currency currency, long unitAmount,
            string customerEmail, bool sendEmailAfterSuccess = false, string emailDescription = "");

        Task ChargeWithCustomerEmail(string customerEmail, string paymentMethodId, Currency currency, long unitAmount,
            bool sendEmailAfterSuccess = false, string emailDescription = "");

        Task<IEnumerable<ChargeModel>> GetPaymentStatus(string paymentId);
    }
}
