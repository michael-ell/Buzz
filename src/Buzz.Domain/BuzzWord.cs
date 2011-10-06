﻿using Buzz.Events;
using Ncqrs.Domain;

namespace Buzz.Domain
{
    public class Buzzword : AggregateRootMappedByConvention
    {
        private string _word;

        public Buzzword(string word)
        {
            var e = new BuzzwordFoundEvent(word);
            ApplyEvent(e);
        }

        protected void OnBuzzWordFound(BuzzwordFoundEvent e)
        {
            _word = e.Word;
        }
    }
}