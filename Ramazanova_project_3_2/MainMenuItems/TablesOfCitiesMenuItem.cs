using System.Globalization;
using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;
using Ramazanova_project_3_2.WeatherDirectory;
using Spectre.Console;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню с выводом таблицы.
    /// </summary>
    public class TablesOfCitiesMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Вывести таблицу с городами.";
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
            
            Table table = new();
            table.AddColumn("Название");
            table.AddColumn("Страна");
            table.AddColumn("Население");
            table.AddColumn("Широта");
            table.AddColumn("Долгота");
            table.AddColumn("Описание погоды");
            table.AddColumn("Температура, °C");
            table.AddColumn("Ощущается как, °C");
            table.AddColumn("Влажность, %");
            table.AddColumn("Облачность, %");
            table.AddColumn("Скорость ветра, м/с");
            table.AddColumn("Осадки, мм");
            table.AddColumn("Тип объекта; адрес");
            

            foreach (City city in CityData.Cities)
            {
                AddRow(city, table);
            }
            
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Метод для добавления ряда в таблицу
        /// </summary>
        /// <param name="city">Город</param>
        /// <param name="table">Таблица</param>
        private static void AddRow(City city, Table table)
        {
            Weather cityWeather = city.GetCurrentWeather();
            table.AddRow(
                city.Name,
                city.Country,
                city.Population == 0 ? "" : city.Population.ToString(),
                city.Latitude.ToString(CultureInfo.InvariantCulture),
                city.Longitude.ToString(CultureInfo.InvariantCulture),
                cityWeather.Description ?? "",
                cityWeather.Temperature.ToString(CultureInfo.InvariantCulture),
                cityWeather.FeelsLike.ToString(CultureInfo.InvariantCulture),
                cityWeather.Humidity.ToString(CultureInfo.InvariantCulture),
                cityWeather.Cloudiness.ToString(CultureInfo.InvariantCulture),
                cityWeather.WindSpeed.ToString(CultureInfo.InvariantCulture),
                cityWeather.Precipitation.ToString(CultureInfo.InvariantCulture),
                city.GetAddress()
            );
        }
    }
}