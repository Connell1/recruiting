using Flipdish.Recruiting.Domain.Models.Input;
using Flipdish.Recruiting.Services.Services;
using Flipdish.Recruiting.UnitTest.Utils.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Flipdish.Recruiting.UnitTest.Services
{
    public class QueryParsingServiceTests
    {
        private readonly QueryParsingService service;
        public QueryParsingServiceTests()
        {
            service = new QueryParsingService();
        }

        [Fact]
        public void Parse()
        {
            var queryCollection = QueryCollectionFactory.Create().SetupQuery();
            var query = service.Parse(queryCollection.Object);

            Assert.Contains("to1", query.To);
            Assert.Contains("to2", query.To);
            Assert.Equal("eur", query.Currency);
            Assert.Equal("eancode", query.MetadataKey);
            Assert.Contains(1, query.StoreIDs);
            Assert.Contains(2, query.StoreIDs);
        }
    }
}
