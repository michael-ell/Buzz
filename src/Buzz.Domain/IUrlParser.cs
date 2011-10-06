using System.Collections.Generic;

namespace Buzz.Domain
{
    public interface IUrlParser
    {
        IEnumerable<string> Parse(string url);
    }
}