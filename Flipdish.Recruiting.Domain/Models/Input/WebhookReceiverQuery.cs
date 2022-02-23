using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flipdish.Recruiting.Domain.Models.Input
{
    public class WebhookReceiverQuery
    {
        [JsonProperty("to")]
        public IEnumerable<string> To { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("metadatakey")]
        public string MetadataKey { get; set; }
        [JsonProperty("storeid")]
        public IEnumerable<int> StoreID { get; set; }
    }
}
