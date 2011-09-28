using System;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Events
{
    public class BuzzWordFoundEvent : SourcedEvent
    {
        public string Word { get; set; }

        public BuzzWordFoundEvent(string word)
        {
            if (word == null) throw new ArgumentNullException("word");
            Word = word;
        }
    }
}