using System;
using System.Collections.Generic;
using System.Linq;
using Buzz.Domain;
using Buzz.Infrastructure;

namespace Buzz.Mashing
{
    public class UrlParser : IUrlParser
    {
        private readonly IUrlLoader _loader;

        public UrlParser(IUrlLoader loader)
        {
            if (loader == null) throw new ArgumentNullException("loader");
            _loader = loader;
        }

        public IEnumerable<string> Parse(string url)
        {
            var contents = _loader.Load(url) ?? string.Empty;
            var set = new HashSet<string>(Normalize(contents).Split(' '));
            return set.Where(word => word.IsNotEmpty());
        }  

        private string Normalize(string content)
        {
            return content.Replace("\n", " ").Replace("\r", " ");
        }
    }
}