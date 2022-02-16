using Microsoft.Extensions.Logging;
using Stripe.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var res = await this._paymentsGateway.PopulatePlans(new List<PlanModel>() {
                new PlanModel()
                {
                    Name = "basic",
                    Prices = new List<PlanPriceModel>()
                    {
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Monthly,
                            Currency = Currency.USD,
                            UnitAmount = 10000
                        },
                        new PlanPriceModel()
                        {
                            Interval = PriceInterval.Yearly,
                            Currency = Currency.USD,
                            UnitAmount = 8000
                        }
                    }
                }
            });
        }
    }
}
