using Buzz.Events;
using Ncqrs.Domain;

namespace Buzz.Domain
{
    public class BuzzWord : AggregateRootMappedByConvention
    {
        private string _word;

        public BuzzWord(string word)
        {
            var e = new BuzzWordFoundEvent(word);
            ApplyEvent(e);
        }

        protected void OnBuzzWordFound(BuzzWordFoundEvent e)
        {
            _word = e.Word;
        }
    }
}