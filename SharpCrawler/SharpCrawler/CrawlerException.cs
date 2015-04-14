using System;

namespace SharpCrawler
{
    class CrawlerException: Exception
    {
        public CrawlerException(string message)
            : base(message)
        { }
    }
}
