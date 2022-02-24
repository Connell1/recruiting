using Flipdish.Recruiting.Domain.Models;
using Flipdish.Recruiting.Domain.Models.Output;
using Microsoft.Extensions.Logging;

namespace Flipdish.Recruiting.Services.Services
{
    public interface IEmailRenderingService
    {
        EmailRenderingResult RenderEmailOrder(ILogger log, EmailRenderingOptions renderingOptions);
    }

    internal class EmailRenderingService : IEmailRenderingService
    {
        public EmailRenderingResult RenderEmailOrder(ILogger log, EmailRenderingOptions renderingOptions)
        {
            using var emailRenderer = new EmailRenderer(renderingOptions.Order, renderingOptions.AppId, renderingOptions.MetadataKey, renderingOptions.AppDirectory, log, renderingOptions.Currency);
            return new EmailRenderingResult(emailRenderer.RenderEmailOrder(), emailRenderer._imagesWithNames);
        }
    }
}
