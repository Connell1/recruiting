using System;
using System.Collections.Generic;
using System.Text;

namespace Flipdish.Recruiting.Domain.Models
{
    public class EmailRenderingOptions
    {
        public Order Order { get; set; }
        public string AppId { get; set; }
        public string MetadataKey { get; set; }
        public string AppDirectory { get; set; }
        public Currency Currency { get; set; }
    }
}
