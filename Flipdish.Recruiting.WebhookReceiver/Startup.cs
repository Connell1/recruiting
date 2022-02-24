﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Flipdish.Recruiting.Services.Extensions;
using MediatR;

[assembly: FunctionsStartup(typeof(Flipdish.Recruiting.WebhookReceiver.Startup))]
namespace Flipdish.Recruiting.WebhookReceiver
{
    internal class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddMediatR(typeof(Startup))
                .AddServices();
        }
    }
}


