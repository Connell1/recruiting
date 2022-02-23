using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Flipdish.Recruiting.WebhookReceiver.Startup))]
namespace Flipdish.Recruiting.WebhookReceiver
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
        }
    }
}


