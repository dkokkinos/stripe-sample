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
    public class PlansController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentsGateway _paymentsGateway;

        public PlansController(IPaymentsGateway paymentsGateway,
            ILogger<HomeController> logger)
        {
            _paymentsGateway = paymentsGateway;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _paymentsGateway.GetPlans();
            return View(plans);
        }

        public async Task<ActionResult> PopulatePlans()
        {
            var createdPlans = await this._paymentsGateway.PopulatePlans(new List<Web.Services.PlanDto>() {
                new Web.Services.PlanDto()
                {
                    Name = "basic",
                    Prices = new List<PlanPriceModel>()
                    {
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Day,
                            Currency = Currency.USD,
                            UnitAmount = 10000
                        },
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Year,
                            Currency = Currency.USD,
                            UnitAmount = 8000
                        }
                    }
                },
                new Web.Services.PlanDto()
                {
                    Name = "premium",
                    Prices = new List<PlanPriceModel>()
                    {
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Day,
                            Currency = Currency.USD,
                            UnitAmount = 170000
                        },
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Year,
                            Currency = Currency.USD,
                            UnitAmount = 200000
                        }
                    }
                },
                new Web.Services.PlanDto()
                {
                    Name = "professional",
                    Prices = new List<PlanPriceModel>()
                    {
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Day,
                            Currency = Currency.USD,
                            UnitAmount = 100000
                        },
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Year,
                            Currency = Currency.USD,
                            UnitAmount = 800000
                        }
                    }
                },
                new Web.Services.PlanDto()
                {
                    Name = "free",
                    Prices = new List<PlanPriceModel>()
                }
            });
            return View("Index", createdPlans);
        }

    }
}
