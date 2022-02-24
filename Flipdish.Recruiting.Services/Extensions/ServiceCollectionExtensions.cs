using Flipdish.Recruiting.Domain.Commands;
using Flipdish.Recruiting.Services.CommandHandlers;
using Flipdish.Recruiting.Services.Services;
using MediatR;
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
                .AddTransient<Services.IEmailRenderingService, Services.EmailRenderingService>()
                .AddCommandHandlers();
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            return services              
                .AddScoped<Flipdish.Recruiting.Domain.Bus.IMediator, InMemoryBus>()
                .AddScoped<IRequestHandler<SendEmailCommand, SendEmailCommandResponse>, SendEmailCommandHandler>();
        }
    }
}
