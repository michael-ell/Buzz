using System.Collections.Generic;
using Buzz.Mashing;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Buzz.Specs.Mashing
{
    [Binding]
    public class ParsingWebPagesSteps
    {
        private string _url;
        private IEnumerable<string> _words;

        [Given(@"I have a valid url")]
        public void GivenIHaveAValidUrl()
        {
            _url = "http://scotthanselman.com";
        }

        [When(@"I parse the content")]
        public void WhenIParseTheContent()
        {
            var loader = new LynxUrlLoader(new LynxUrlLoader.LynxInfo { ExecutablePath = @"c:\lynx_w32\lynx.exe", ConfigPath = @"c:\lynx_w32\lynx.cfg" });
            var parser = new UrlParser(loader);
            _words = parser.Parse(_url);
        }

        [Then(@"raw words should be returned")]
        public void ThenRawWordsShouldBeReturned()
        {
            _words.Should().NotBeEmpty();
        }
    }
}
