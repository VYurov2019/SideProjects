using System;
using System.Runtime.Serialization;
using TravelAssistant.Domain.Entities;

namespace TravelAssistant.Domain.Exceptions
{
    [Serializable]
    public class TaCircularReferenceException : Exception
    {
        public TaCitiesPair CitiesPair { get; }

        public TaCircularReferenceException() : this((TaCitiesPair)null)
        {
        }
        public TaCircularReferenceException(string message) : this(null, message)
        {

        }
        public TaCircularReferenceException(string message, Exception inner) : this((TaCitiesPair)null, message, inner)
        {
        }

        public TaCircularReferenceException(TaCitiesPair citiesPair) : this(citiesPair, null, null)
        {
        }

        public TaCircularReferenceException(TaCitiesPair citiesPair, string message) : this(citiesPair, message, null)
        {

        }

        public TaCircularReferenceException(TaCitiesPair citiesPair, string message, Exception inner) : base(message, inner)
        {
            CitiesPair = citiesPair;
        }

        protected TaCircularReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
