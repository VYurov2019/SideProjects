using System;
using System.Runtime.Serialization;
using TravelAssistant.Domain.Entities;

namespace TravelAssistant.Domain.Exceptions
{
    [Serializable]
    public class TaAddingDuplicateException : Exception
    {
        public TaCitiesPair CitiesPair { get; }

        public TaAddingDuplicateException() : this((TaCitiesPair)null)
        {
        }
        public TaAddingDuplicateException(string message) : this(null, message)
        {

        }
        public TaAddingDuplicateException(string message, Exception inner) : this((TaCitiesPair)null, message, inner)
        {
        }

        public TaAddingDuplicateException(TaCitiesPair citiesPair) : this(citiesPair, null, null)
        {
        }

        public TaAddingDuplicateException(TaCitiesPair citiesPair, string message) : this(citiesPair, message, null)
        {

        }

        public TaAddingDuplicateException(TaCitiesPair citiesPair, string message, Exception inner) : base(message, inner)
        {
            CitiesPair = citiesPair;
        }

        protected TaAddingDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
