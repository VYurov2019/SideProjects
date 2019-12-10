using TravelAssistant.Domain.Entities;
using TravelAssistant.Logic.Services;

namespace TravelAssistant.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // данный проект использовался для отладки алгоритма

            var manager = new TravelAssistantService();

            var referenceResult = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Москва", "Париж"),
            };
			
            var referenceInput = new[] {
                new TaCitiesPair("Кельн", "Москва"),                
                new TaCitiesPair("Мельбурн", "Кельн"),                
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Берлин", "Мельбурн"),
            };

            var debugSample = new[] {
                new TaCitiesPair("Берлин", "Кельн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Кельн", "Москва"),
            };

            manager.SortCitiesPairs(debugSample);
        }
    }
}
