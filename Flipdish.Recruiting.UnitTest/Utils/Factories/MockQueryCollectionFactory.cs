using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;

namespace Flipdish.Recruiting.UnitTest.Utils.Factories
{
    internal static class MockQueryCollectionFactory
    {
        public static Mock<IQueryCollection> Create()
        {
            return new Mock<IQueryCollection>();
        }

        public static Mock<IQueryCollection> SetupQuery(this Mock<IQueryCollection> mock)
        {
            var to = new StringValues(new string[] { "to1", "to2" });
            var currency = new StringValues("eur");
            var metadataKey = new StringValues("eancode");
            var storeIds = new StringValues(new string[] { "1", "2" });

            mock.Setup(m => m.ContainsKey(It.IsAny<string>())).Returns(true);
            mock.SetupGet(m=> m["to"]).Returns(to);
            mock.SetupGet(m=> m["currency"]).Returns(currency);
            mock.SetupGet(m=> m["metadataKey"]).Returns(metadataKey);
            mock.SetupGet(m=> m["storeId"]).Returns(storeIds);

            return mock;
        }
    }
}
