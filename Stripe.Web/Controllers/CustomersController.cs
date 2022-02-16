using Microsoft.AspNetCore.Mvc;
using Stripe.Web.Models.Customers;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IPaymentsGateway _paymentsGateway;

        public CustomersController(IPaymentsGateway paymentsGateway)
        {
            _paymentsGateway = paymentsGateway;
        }

        public async Task<ActionResult<List<CustomerModel>>> Index()
        {
            var customers = await this._paymentsGateway.GetCustomers(take: 10);
            return View(customers);
        }

        public async Task<IActionResult> Create(CreateCustomerModel model)
        {
            var createdCustomer = await this._paymentsGateway.CreateCustomer(model.Name, model.Email, model.SystemId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(DeleteCustomerModel model)
        {
            var customer = await this._paymentsGateway.DeleteCustomerByEmail(model.Email);
            return RedirectToAction("Index");
        }

    }
}
