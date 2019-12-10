using NUnit.Framework;
using TravelAssistant.Domain.Entities;
using TravelAssistant.Domain.Exceptions;
using TravelAssistant.Logic.Services;

namespace TravelAssistant.Tests
{
    [TestFixture]
    class TravelAssistantServiceTests
    {
        [Test]
        public void SortCitiesPairs_CorrectInput_ShouldSuccess()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Мельбурн", "Кельн"),
            };

            var expectedResult = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Москва", "Париж"),
            };            

            //Act
            manager.SortCitiesPairs(input);

            //Assert
            Assert.That(expectedResult, Is.EqualTo(input));
        }

        [Test()]        
        public void SortCitiesPairs_InputWithRepetitionsOfCities_ShouldThrowTaAddingDuplicateException1()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Берлин", "Кельн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Кельн", "Москва"),
            };

            //Act
            //Assert
            Assert.Throws<TaAddingDuplicateException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithRepetitionsOfCities_ShouldThrowTaAddingDuplicateException2()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Берлин", "Кельн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Москва", "Париж"),
            };

            //Act
            //Assert
            Assert.Throws<TaAddingDuplicateException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithRepetitionsOfCities_ShouldThrowTaAddingDuplicateException3()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Кельн", "Мельбурн"),
                new TaCitiesPair("Москва", "Париж"),
            };

            //Act
            //Assert
            Assert.Throws<TaAddingDuplicateException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithRepetitionsOfCities_ShouldThrowTaAddingDuplicateException4()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Париж", "Кельн"),
            };

            //Act
            //Assert
            Assert.Throws<TaAddingDuplicateException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithSelfReference_ShouldThrowTaCircularReferenceException()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Кельн", "Москва"),
                new TaCitiesPair("Берлин", "Берлин"),
                new TaCitiesPair("Москва", "Париж"),
            };

            //Act
            //Assert
            Assert.Throws<TaCircularReferenceException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithClosedChain_ShouldThrowTaCircularReferenceException()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Париж", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Москва", "Париж"),
                new TaCitiesPair("Кельн", "Москва"),
            };

            //Act
            //Assert
            Assert.Throws<TaCircularReferenceException>(() => manager.SortCitiesPairs(input));
        }

        [Test]
        public void SortCitiesPairs_InputWithMultipleChains_ShouldThrowTaManyHeadsException()
        {
            //Arrange
            var manager = new TravelAssistantService();

            var input = new[] {
                new TaCitiesPair("Берлин", "Мельбурн"),
                new TaCitiesPair("Мельбурн", "Кельн"),
                new TaCitiesPair("Москва", "Париж"),
            };

            //Act
            //Assert
            Assert.Throws<TaManyHeadsException>(() => manager.SortCitiesPairs(input));
        }
    }
}
