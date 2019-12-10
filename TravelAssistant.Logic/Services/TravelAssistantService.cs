using System;
using System.Collections.Generic;
using System.Linq;
using TravelAssistant.Domain.Entities;
using TravelAssistant.Domain.Exceptions;
using TravelAssistant.Domain.Interfaces;

namespace TravelAssistant.Logic.Services
{
    public class TravelAssistantService : ITravelAssistantService
    {
        #region Комментарии к решению
        /*
            1 Весь алгоритм делаем однопоточным. Многопоточная реализация в лоб через один ConcurentDictionary не даст выйгрыша. 
            Т.к. у нас тут нету интенсивных IO-Bound/CPU-Bound операций. Но зато очень много Memory-Bound операций.
            В этой ситуации потоки будут постоянно упираться в блокировки друг от друга.            
            Можно попробовать реализовать многопоточный алгоритм, когда массив делится на части и каждую часть парсит отдельный поток, а затем 
            конкатенировать результат их работы. Но по условиям задачи нам не требуется сверхоптимизация.

            2 Алгоритм написан с упором на скорость в ущерб объему занимаемой памяти.
            В наше время большие объемы оперативной памяти не редкость. Хотя, конечно, многое может зависеть от объема реальных данных.
        */
        #endregion

        public void SortCitiesPairs(TaCitiesPair[] citiesPairs)
        {
            //todo: уточнить какое поведение должно быть в реальности
            if (citiesPairs == null || citiesPairs.Length == 0)
                throw new ArgumentException(nameof(citiesPairs));

            /*
             Входящий массив citiesPairs будем сохранять в хэш-таблице (сложность алгоритма O(1) в случае корректной хэш функции, и общего числа элементов коллекции, меньшего int.MaxValue)
             Затем будем использовать этот набор для быстрого составления сортированного списка карточек.
             Сразу выделим всю необходимую память, чтобы не создавать memory pressure
             */
            Dictionary<string, TaCitiesPair> allCities = new Dictionary<string, TaCitiesPair>(citiesPairs.Length); // размерность аналогична размерности входящего массива
            
            /*
              Словарь хранит информацию о встретившихся городах, с учетом того - ГородОткуда или ГородКуда (перегружена операция Equals).
              Так мы защитимся от появления дублей за единственный проход по списку (в ущерб памяти).
            */
            Dictionary<TaCity, object> metСities = new Dictionary<TaCity, object>(citiesPairs.Length * 2); // размерность в 2 раза больше размерности входящего массива, т.к. фактически разбиваем каждую карточку на две.
            
            /*
             Еще одна хэш-таблица для поиска начала списка городов. Чтобы не пришлось в конце делать полный перебор городов в поисках того, на который не ссылаются.
             Будем добавлять города при первой встрече и извлекать, после нахождения на них ссылок.
             Далее в алгоритме учтены ситуации, когда ссылка на город встретилась раньше самого города.      
             
             В самом худшем варианте размер таблицы будет равен половине размерности входящего массива.
             Сразу выделим память на худший вариант, чтобы не создавать memory pressure.  Это решение несколько спорное, и требует тестов на реальных данных,
             возможно можно подобрать меньшее число не жертвуя скоростью алгоритма на худших вариантах входных данных.
            */
            Dictionary<string, object> headCities= new Dictionary<string, object>(citiesPairs.Length / 2);

            // Если в headCities храним города, на которые не нашлось ссылки. То тут храним города, для которых не нашли на кого они ссылаются.
            Dictionary<string, object> citiesWithoutRef = new Dictionary<string, object>();

            foreach (TaCitiesPair citiesPair in citiesPairs)
            {
                if (citiesPair.CityFrom == citiesPair.CityTo)
                    throw new TaCircularReferenceException(citiesPair);

                /*
                   Решение через try catch выйдет быстрее чем 
                    if (!metСities.ContainsKey(...))
                        metСities.Add(...)
                    else
                      throw
                                        
                  Также try catch внутри foreach позволит узнать конкретную проблемную пару городов. Хотя это и выйдет немного
                  менее эффективно, чем try catch поверх foreach (за счет постоянного создания SEH), но зато позволит пользователям быстрее находить проблемные места в данных
                 */
                try
                {
                    metСities.Add(new TaCity(
                                cityName: citiesPair.CityFrom,
                                isCityFrom: true)
                                , null);
                    metСities.Add(new TaCity(
                                cityName: citiesPair.CityTo,
                                isCityFrom: false)
                                , null);
                }
                catch (ArgumentException)
                {
                    throw new TaAddingDuplicateException(citiesPair);
                }

                headCities.Add(citiesPair.CityFrom, null);

                if (!headCities.Remove(citiesPair.CityTo))
                    // список городов CityTo для которых пока не нашлось городов CityFrom
                    citiesWithoutRef.Add(citiesPair.CityTo, null);
                
                allCities.Add(citiesPair.CityFrom, citiesPair);
            }

            // теперь сверяем списки, чтобы найти ссылки между городами, пришедшими не в удачном порядке
            foreach (var city in citiesWithoutRef.Keys)
                headCities.Remove(city);            

            // дополнительные проверки, свидетельствующие о проблемах
            if (!headCities.Any())
                throw new TaCircularReferenceException(); 
            if (headCities.Count > 1)
                throw new TaManyHeadsException(headCities.Keys);


            /*
             Чтобы избежать лишних выделений памяти будем использовать входной параметр citiesPairs.              
            */
            var nextCity = headCities.Single().Key;
            for (var i = 0; i < citiesPairs.Length; i++)
            {
                var currentPair = allCities[nextCity];
                citiesPairs[i] = currentPair;
                nextCity = currentPair.CityTo;
            }
        }
    }
}
