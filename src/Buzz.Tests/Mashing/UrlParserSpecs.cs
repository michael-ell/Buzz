using System.Collections.Generic;
using System.Linq;
using Buzz.Infrastructure;
using Buzz.Mashing;
using Buzz.Tests.BDD;
using FluentAssertions;
using Moq;

namespace Buzz.Tests.Mashing.UrlParserSpecs
{
    [Concern(typeof (UrlParser))]
    public class When_parsing_an_url : ContextBase<UrlParser>
    {
        private string _expectedUrl;

        protected override void Given()
        {
            _expectedUrl = "www.xyz.com";
        }

        protected override void When()
        {
            Sut.Parse(_expectedUrl);
        }

        [Observation]
        public void Then_should_load_the_url()
        {
            MockFor<IUrlLoader>().Verify(loader => loader.Load(_expectedUrl));
        }
    }

    [Concern(typeof (UrlParser))]
    public class When_parsing_nothing : ContextBase<UrlParser>
    {
        private IEnumerable<string> _words;

        protected override void Given()
        {
            MockFor<IUrlLoader>().Setup(loader => loader.Load(It.IsAny<string>())).Returns((string) null);
        }

        protected override void When()
        {
            _words = Sut.Parse("www.xyz.com");
        }

        [Observation]
        public void Then_should_return_an_empty_words_result()
        {
            _words.Should().NotBeNull();
            _words.Should().HaveCount(0);
        }
    }

    [Concern(typeof(UrlParser))]
    public class When_parsing_content_with_duplicate_words : ContextBase<UrlParser>
    {
        private IEnumerable<string> _expectedWords;
        private IEnumerable<string> _words;

        protected override void Given()
        {
            const string contentToReturn = "The red red fox fox";
            _expectedWords = new List<string> {"The", "red", "fox"};
            MockFor<IUrlLoader>().Setup(loader => loader.Load(It.IsAny<string>())).Returns(contentToReturn);
        }

        protected override void When()
        {
            _words = Sut.Parse("www.abc.com");
        }

        [Observation]
        public void Then_should_only_return_the_unique_words()
        {
            _words.Should().Contain(_expectedWords);
        }
    }

    [Concern(typeof(UrlParser))]
    public class When_parsing_content_that_contains_line_feeds : ContextBase<UrlParser>
    {
        private IEnumerable<string> _expectedWords;
        private IEnumerable<string> _words;

        protected override void Given()
        {
            const string contentToReturn = "The\nred\nfox\njumped\nover";
            _expectedWords = new List<string> { "The", "red", "fox", "jumped", "over" };
            MockFor<IUrlLoader>().Setup(loader => loader.Load(It.IsAny<string>())).Returns(contentToReturn);
        }

        protected override void When()
        {
            _words = Sut.Parse("www.abc.com");
        }

        [Observation]
        public void Then_should_not_return_the_line_feeds()
        {
            _words.Should().Contain(_expectedWords);
        }
    }

    [Concern(typeof(UrlParser))]
    public class When_parsing_content_that_contains_carriage_returns : ContextBase<UrlParser>
    {
        private IEnumerable<string> _expectedWords;
        private IEnumerable<string> _words;

        protected override void Given()
        {
            const string contentToReturn = "The\rred\rfox\rjumped\rover";
            _expectedWords = new List<string> { "The", "red", "fox", "jumped", "over" };
            MockFor<IUrlLoader>().Setup(loader => loader.Load(It.IsAny<string>())).Returns(contentToReturn);
        }

        protected override void When()
        {
            _words = Sut.Parse("www.abc.com");
        }

        [Observation]
        public void Then_should_not_return_the_carriage_returns()
        {
            _words.Should().Contain(_expectedWords);
        }
    }

    [Concern(typeof(UrlParser))]
    public class When_parsing_content_spaces_should_not_be_returned: ContextBase<UrlParser>
    {
        private IEnumerable<string> _words;

        protected override void Given()
        {
            const string contentToReturn = "The  red         fox       jumped      over";
            MockFor<IUrlLoader>().Setup(loader => loader.Load(It.IsAny<string>())).Returns(contentToReturn);
        }

        protected override void When()
        {
            _words = Sut.Parse("www.abc.com");
        }

        [Observation]
        public void Then_should_not_return_any_empty_words()
        {
            _words.Where(word => word.IsEmpty()).Should().HaveCount(0);
        }
    }
}