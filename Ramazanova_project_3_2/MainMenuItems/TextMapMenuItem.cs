using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню с выводом текстовой карты.
    /// </summary>
    public class TextMapMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Вывести текстовую карту городов";
        /// <summary>
        /// Ширина карты
        /// </summary>
        private const int MapWidth = 80;
        /// <summary>
        /// Высота карты
        /// </summary>
        private const int MapHeight = 20;
        /// <summary>
        /// Задача, которую выполняет элемент меню.
        /// </summary>
        public void Select()
        {
            if (!CityData.Cities.Any())
            {
                Console.WriteLine("Введите данные о городах.");
                return;
            }
            
            char[,] map = GetMap();


            foreach (City city in CityData.Cities)
            {
                int x = (int)((city.Longitude + 180) / 360 * MapWidth);

                int y = (int)((90 - city.Latitude) / 180 * MapHeight);

                if (x is >= 0 and < MapWidth && y is >= 0 and < MapHeight)
                {
                    map[y, x] = city.Name[0];
                }
            }

            PrintMap(map);

            PrintLegend();
        }
        
        /// <summary>
        /// Метод для создания карты.
        /// </summary>
        /// <returns>Массив с символами карты</returns>
        private static char[,] GetMap()
        {
            char[,] map = new char[MapHeight, MapWidth];

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    map[y, x] = ' ';
                }
            }

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (x % 20 == 0 || y % 5 == 0)
                    {
                        map[y, x] = '.'; // Сетка
                    }
                }
            }

            map[0, MapWidth / 2] = 'N';
            map[MapHeight - 1, MapWidth / 2] = 'S';
            map[MapHeight / 2, 0] = 'W';
            map[MapHeight / 2, MapWidth - 1] = 'E';

            return map;
        }
        
        /// <summary>
        /// Метод для распечатывания карты.
        /// </summary>
        /// <param name="map">Массив с символами карты</param>
        private static void PrintMap(char[,] map)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (map[y, x] == 'N' || map[y, x] == 'S' || map[y, x] == 'W' || map[y, x] == 'E')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Стороны света выделяем желтым
                    }
                    else if (map[y, x] == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray; // Сетка серым
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Города выделяем красным
                    }

                    Console.Write(map[y, x]);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }
        /// <summary>
        /// Метод для распечатки легенды карты.
        /// </summary>
        private static void PrintLegend()
        {
            Console.WriteLine("Обозначения на карте:");
            Console.WriteLine("N - север, S - юг, W - запад, E - восток");
            Console.WriteLine("Города:");
            foreach (City city in CityData.Cities)
            {
                Console.WriteLine(
                        $"    > {city.Name[0]} - {city.Name} (ширина: {city.Latitude}, долгота: {city.Longitude})");
            }
        }
    }
}