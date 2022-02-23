using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Flipdish.Recruiting.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<Services.IEmailService, Services.EmailService>()
                .AddTransient<Services.IQueryParsingService, Services.QueryParsingService>()
                .AddTransient<Services.IEmailRenderingService, Services.EmailRenderingService>();
        }
    }
}
