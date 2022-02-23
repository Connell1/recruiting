using Flipdish.Recruiting.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Flipdish.Recruiting.Services.Services
{
    public interface IEmailRenderingService
    {
        void CreateRenderer(ILogger log, EmailRenderingOptions renderingOptions);
        string RenderEmailOrder();
        Dictionary<string, Stream> ImagesWithName { get; }
    }

    internal class EmailRenderingService : IEmailRenderingService, IDisposable
    {
        private EmailRenderer emailRenderer;
        private bool disposedValue;

        public Dictionary<string, Stream> ImagesWithName => emailRenderer._imagesWithNames;

        public void CreateRenderer(ILogger log, EmailRenderingOptions renderingOptions)
        {
            emailRenderer = new EmailRenderer(renderingOptions.Order, renderingOptions.AppId, renderingOptions.MetadataKey, renderingOptions.AppDirectory, log, renderingOptions.Currency);
        }

        public string RenderEmailOrder()
        {
            return emailRenderer.RenderEmailOrder();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    emailRenderer.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
