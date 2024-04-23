namespace Stripe.Web.Models.Subscriptions
{
    public class CustomerModel
    {
        public string Email { get; }

        public CustomerModel(string email)
        {
            Email = email;
        }
    }
}
