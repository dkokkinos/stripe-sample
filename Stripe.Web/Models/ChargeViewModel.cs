using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Models
{
    public class ChargeViewModel
    {
        public string StripeToken { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
    }
}
