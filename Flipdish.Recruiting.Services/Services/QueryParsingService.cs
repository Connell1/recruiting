using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Flipdish.Recruiting.Domain.Models.Input;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Flipdish.Recruiting.Services.Services
{
    public interface IQueryParsingService
    {
        T Parse<T>(IQueryCollection queryCollection);
        WebhookReceiverQuery Parse(IQueryCollection queryCollection);
    }

    internal class QueryParsingService : IQueryParsingService
    {
        public T Parse<T>(IQueryCollection queryCollection)
        {
            var dict = new Dictionary<string, object>();
            var props = typeof(T).GetProperties();
            foreach (var property in props)
            {
                if (queryCollection.ContainsKey(property.Name.ToLower()))
                {
                    var val = queryCollection[property.Name.ToLower()];
                    if (property.PropertyType is IEnumerable)
                        dict.Add(property.Name, val);
                    else
                        dict.Add(property.Name, val.FirstOrDefault());
                }
            }
            string json = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public WebhookReceiverQuery Parse(IQueryCollection queryCollection)
        {
            List<int> storeIds = new List<int>();
            string[] storeIdParams = queryCollection["storeId"].ToArray();
            if (storeIdParams.Length > 0)
            {
                foreach (var storeIdString in storeIdParams)
                    if (int.TryParse(storeIdString, out int storeId))
                        storeIds.Add(storeId);
            }

            return new WebhookReceiverQuery()
            {
                To = queryCollection["to"].ToArray(),
                StoreIDs = storeIds,
                Currency = queryCollection["currency"].FirstOrDefault(),
                MetadataKey = queryCollection["metadataKey"].First() ?? "eancode"
            };
        }
    }
}
