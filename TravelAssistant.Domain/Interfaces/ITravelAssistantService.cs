using TravelAssistant.Domain.Entities;

namespace TravelAssistant.Domain.Interfaces
{
    public interface ITravelAssistantService
    {
        void SortCitiesPairs(TaCitiesPair[] citiesPairs);
    }
}
