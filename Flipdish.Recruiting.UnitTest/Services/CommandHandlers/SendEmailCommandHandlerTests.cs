using Flipdish.Recruiting.Domain.Commands;
using Flipdish.Recruiting.Domain.Models;
using Flipdish.Recruiting.Services.CommandHandlers;
using Flipdish.Recruiting.Services.Services;
using Flipdish.Recruiting.UnitTest.Utils.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Flipdish.Recruiting.UnitTest.Services.CommandHandlers
{
    public class SendEmailCommandHandlerTests
    {
        private readonly Mock<IEmailRenderingService> mockEmailService;
        private readonly SendEmailCommandHandler sendEmailCommandHandler;

        public SendEmailCommandHandlerTests()
        {
            mockEmailService = MockEmailRenderingServiceFactory.Create();
            sendEmailCommandHandler = new SendEmailCommandHandler(new Mock<ILogger<SendEmailCommandHandler>>().Object, mockEmailService.Object);
        }

        [Fact]
        public async Task Hanlde()
        {
            mockEmailService.SetupRenderEmailOrder();
            var command = new SendEmailCommand()
            {
                EmailRenderingOptions = new EmailRenderingOptions()
                {
                    AppId = "123"
                }
            };

            await sendEmailCommandHandler.Handle(command, CancellationToken.None);
            mockEmailService.Verify(x => x.RenderEmailOrder(It.IsAny<ILogger<SendEmailCommandHandler>>(), It.Is<EmailRenderingOptions>(x => x.AppId == command.EmailRenderingOptions.AppId)));
        }
    }
}
