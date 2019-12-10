using System.Diagnostics;

namespace TravelAssistant.Domain.Entities
{
    [DebuggerDisplay("CityFrom = {CityFrom}; CityTo = {CityTo}")]
    public class TaCitiesPair
    {
        public string CityFrom { get; set; }
        public string CityTo { get; set; }

        public TaCitiesPair(string cityFrom, string cityTo)
        {
            CityFrom = cityFrom;
            CityTo = cityTo;
        }        

        public override bool Equals(object obj)
        {
            var other = (TaCitiesPair)obj;
            if (other == null)
                return false;
            var result = this.CityFrom == other.CityFrom && this.CityTo == other.CityTo;
            return result;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + CityFrom == null ? 0 : CityFrom.GetHashCode();
            hash = hash * 23 + CityTo == null ? 0 : CityTo.GetHashCode();
            return hash;
        }
    }
}
