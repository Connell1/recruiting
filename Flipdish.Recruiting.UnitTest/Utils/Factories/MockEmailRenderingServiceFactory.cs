using Flipdish.Recruiting.Domain.Models;
using Flipdish.Recruiting.Domain.Models.Output;
using Flipdish.Recruiting.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flipdish.Recruiting.UnitTest.Utils.Factories
{
    public static class MockEmailRenderingServiceFactory
    {
        public static Mock<IEmailRenderingService> Create()
        {
            return new Mock<IEmailRenderingService>();
        }

        public static Mock<IEmailRenderingService> SetupRenderEmailOrder(this Mock<IEmailRenderingService> mock)
        {
            mock.Setup(s => s.RenderEmailOrder(It.IsAny<ILogger>(), It.IsAny<EmailRenderingOptions>()))
                .Returns(new EmailRenderingResult("email content", new Dictionary<string, System.IO.Stream>()));
            return mock;
        }
    }
}
