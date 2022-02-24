using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Flipdish.Recruiting.Domain.Models;
using System.Collections.Generic;
using Flipdish.Recruiting.Services.Services;
using Flipdish.Recruiting.Domain.Models.Input;
using Flipdish.Recruiting.Domain.Models.Output;
using Flipdish.Recruiting.Domain.Bus;
using Flipdish.Recruiting.Domain.Commands;

namespace Flipdish.Recruiting.WebhookReceiver.Functions
{
    public class WebhookReceiver
    {
        private readonly IEmailService emailService;
        private readonly IQueryParsingService queryParsingService;
        private readonly IMediator mediator;

        public WebhookReceiver(IEmailService emailService,
            IQueryParsingService queryParsingService,
            IMediator mediator)
        {
            this.emailService = emailService;
            this.queryParsingService = queryParsingService;
            this.mediator = mediator;
        }

        [FunctionName("WebhookReceiver")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            int? orderId = null;
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                OrderCreatedEvent orderCreatedEvent = await GetOrderCreatedWebhook(req, context);
                WebhookReceiverQuery query = queryParsingService.Parse(req.Query);

                orderId = orderCreatedEvent.Order.OrderId;

                if (query.StoreIDs.Any())
                {
                    if (!query.StoreIDs.Contains(orderCreatedEvent.Order.Store.Id.Value))
                    {
                        log.LogInformation($"Skipping order #{orderId}");
                        return new ContentResult { Content = $"Skipping order #{orderId}", ContentType = "text/html" };
                    }
                }

                Currency currency = Currency.EUR;
                var currencyString = query.Currency;
                if(!string.IsNullOrEmpty(currencyString) && Enum.TryParse(typeof(Currency), currencyString.ToUpper(), out object currencyObject))
                {
                    currency = (Currency)currencyObject;
                }

                EmailRenderingResult emailOrder = (await mediator.SendCommand<SendEmailCommand, SendEmailCommandResponse>(new SendEmailCommand()
                {
                    EmailRenderingOptions = new EmailRenderingOptions()
                    {
                        Order = orderCreatedEvent.Order,
                        AppId = orderCreatedEvent.AppId,
                        MetadataKey = query.MetadataKey,
                        AppDirectory = context.FunctionAppDirectory,
                        Currency = currency
                    }
                })).EmailRenderingResult;
                //EmailRenderingResult emailOrder = emailRenderingService.RenderEmailOrder(log, new EmailRenderingOptions()
                //{
                //    Order = orderCreatedEvent.Order,
                //    AppId = orderCreatedEvent.AppId,
                //    MetadataKey = query.MetadataKey,
                //    AppDirectory = context.FunctionAppDirectory,
                //    Currency = currency
                //});

                try
                {
                    await emailService.Send("", query.To, $"New Order #{orderId}", emailOrder.Content, emailOrder.Images);
                }
                catch(Exception ex)
                {
                    log.LogError($"Error occured during sending email for order #{orderId}" + ex);
                }

                log.LogInformation($"Email sent for order #{orderId}.", new { orderCreatedEvent.Order.OrderId });

                return new ContentResult { Content = emailOrder.Content, ContentType = "text/html" };
            }
            catch(Exception ex)
            {
                log.LogError(ex, $"Error occured during processing order #{orderId}");
                throw ex;
            }
        }

        private async Task<OrderCreatedEvent> GetOrderCreatedWebhook(HttpRequest req, ExecutionContext context)
        {
            OrderCreatedWebhook orderCreatedWebhook;

            string testFile = req.Query["test"];
            if (req.Method == HttpMethods.Get && !string.IsNullOrEmpty(testFile))
            {

                var templateFilePath = Path.Combine(context.FunctionAppDirectory, "TestWebhooks", testFile);
                var testWebhookJson = new StreamReader(templateFilePath).ReadToEnd();

                orderCreatedWebhook = JsonConvert.DeserializeObject<OrderCreatedWebhook>(testWebhookJson);
            }
            else if (req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                orderCreatedWebhook = JsonConvert.DeserializeObject<OrderCreatedWebhook>(requestBody);
            }
            else
            {
                throw new Exception("No body found or test param.");
            }
            return orderCreatedWebhook.Body;
        }
    }
}
