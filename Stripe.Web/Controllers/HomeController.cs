using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe.Web.Models;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentsGateway _paymentsGateway;

        public HomeController(IPaymentsGateway paymentsGateway,
            ILogger<HomeController> logger)
        {
            _paymentsGateway = paymentsGateway;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
