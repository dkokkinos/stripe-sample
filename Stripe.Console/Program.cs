﻿using Microsoft.Extensions.Logging;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stripe.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            PlanService service = new PlanService(new StripePaymentsGateway(new Loggg(), ""));
            service.Create().GetAwaiter().GetResult();
        }

        private class Loggg : ILogger<StripePaymentsGateway>
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                
            }
        }
    }

    public class PlanService
    {
        private readonly IPaymentsGateway _paymentsGateway;

        public PlanService(IPaymentsGateway paymentsGateway)
        {
            _paymentsGateway = paymentsGateway;
        }

        public async Task Create()
        {
            var res = await this._paymentsGateway.PopulatePlans(new List<Web.Services.PlanDto>() {
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
        }
    }
}
