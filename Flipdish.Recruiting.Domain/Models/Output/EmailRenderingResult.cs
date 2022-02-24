using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Flipdish.Recruiting.Domain.Models.Output
{
    public class EmailRenderingResult
    {
        public EmailRenderingResult(string content, Dictionary<string, Stream> images)
        {
            Content = content;
            Images = images;
        }

        public string Content { get;  }
        public Dictionary<string, Stream> Images { get; }
    }
}
