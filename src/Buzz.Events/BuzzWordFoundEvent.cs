using System;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Events
{
    [Serializable]
    public class BuzzwordFoundEvent : SourcedEvent
    {
        public string Word { get; set; }

        public BuzzwordFoundEvent(string word)
        {
            if (word == null) throw new ArgumentNullException("word");
            Word = word;
        }
    }
}