using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Models.Customers
{
    public class CreateCustomerModel
    {
        public string SystemId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
