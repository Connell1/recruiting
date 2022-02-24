using Flipdish.Recruiting.Domain.Bus;
using Flipdish.Recruiting.Domain.Models;
using Flipdish.Recruiting.Domain.Models.Output;

namespace Flipdish.Recruiting.Domain.Commands
{
    public class SendEmailCommand : ICommand<SendEmailCommandResponse>
    {
        public EmailRenderingOptions EmailRenderingOptions { get; set; }
    }

    public class SendEmailCommandResponse : ICommandResponse
    {
        public EmailRenderingResult EmailRenderingResult { get; set; }
    }
}
