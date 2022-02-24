using Flipdish.Recruiting.Domain.Bus;
using Flipdish.Recruiting.Domain.Commands;
using Flipdish.Recruiting.Services.Services;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Flipdish.Recruiting.Services.CommandHandlers
{
    public class SendEmailCommandHandler : ICommandHandler<SendEmailCommand, SendEmailCommandResponse>
    {
        private readonly ILogger<SendEmailCommandHandler> log;
        private readonly IEmailRenderingService emailRenderingService;

        public SendEmailCommandHandler(ILogger<SendEmailCommandHandler> log, IEmailRenderingService emailRenderingService)
        {
            this.log = log;
            this.emailRenderingService = emailRenderingService;
        }
        public async Task<SendEmailCommandResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var res = emailRenderingService.RenderEmailOrder(log, request.EmailRenderingOptions);
            await Task.CompletedTask;
            return new SendEmailCommandResponse()
            {
                EmailRenderingResult = res,
            };
        }
    }
}
