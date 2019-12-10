using System.Diagnostics;

namespace TravelAssistant.Domain.Entities
{
    [DebuggerDisplay("CityName = {CityName}; IsCityFrom = {IsCityFrom}")]
    public class TaCity
    {
        private readonly object _isCityFrom;

        public TaCity(string cityName, bool isCityFrom)
        {
            CityName = cityName;

            // используем object вместо bool, чтобы избежать боксинга.
            if (isCityFrom)
                _isCityFrom = new object();
        }

        public string CityName { get; }
        public bool IsCityFrom { get { return _isCityFrom != null; } }

        public override bool Equals(object obj)
        {
            var other = (TaCity)obj;
            if (other == null)
                return false;
            var result = this.CityName == other.CityName && this.IsCityFrom == other.IsCityFrom;
            return result;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (CityName == null ? 0 : CityName.GetHashCode());
            hash = hash * 23 + IsCityFrom.GetHashCode();
            return hash;
        }
    }
}
