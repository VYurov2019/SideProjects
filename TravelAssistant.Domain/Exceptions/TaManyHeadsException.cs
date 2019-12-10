using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TravelAssistant.Domain.Exceptions
{
    [Serializable]
    public class TaManyHeadsException : Exception
    {
        public IEnumerable<string> Cities { get; }

        public TaManyHeadsException() : this((IEnumerable<string>)null)
        {
        }
        public TaManyHeadsException(string message) : this(null, message)
        {

        }
        public TaManyHeadsException(string message, Exception inner) : this((IEnumerable<string>)null, message, inner)
        {
        }

        public TaManyHeadsException(IEnumerable<string> cities) : this(cities, null, null)
        {
        }

        public TaManyHeadsException(IEnumerable<string> cities, string message) : this(cities, message, null)
        {

        }

        public TaManyHeadsException(IEnumerable<string> cities, string message, Exception inner) : base(message, inner)
        {
            Cities = cities;
        }

        protected TaManyHeadsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
