using Newtonsoft.Json;
using Ramazanova_project_3_2.CoordinatesDirectory;
using Ramazanova_project_3_2.WeatherDirectory;
using System.Globalization;

namespace Ramazanova_project_3_2.CityDataDirectory
{
    /// <summary>
    /// Класс для города.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Страна города
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Население
        /// </summary>
        public int Population { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }
        
        /// <summary>
        /// Конструктор для города
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="country">Страна</param>
        /// <param name="longitude">Долгота</param>
        /// <param name="latitude">Широта</param>
        /// <param name="population">Население</param>
        [JsonConstructor]
        public City(string name, string country, double longitude, double latitude, int population = 0)
        {
            Name = name;
            Country = country;
            Population = population;
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Крутой конструктор.
        /// </summary>
        public City()
        {
            Name = "MALAY_IS_THE_BEST";
            Country = "MALAY_IS_THE_BEST";
        }

        /// <summary>
        /// Метод для получения актуальной погоды
        /// </summary>
        /// <returns>Объект с погодой</returns>
        public Weather GetCurrentWeather()
        {
            try
            {
                Task<Weather> task = WeatherService.GetWeatherAsync(Name);
                task.Wait();
                Weather currentWeather = task.Result;
                return currentWeather;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось получить данные о погоде. Ошибка: {ex.Message}");
            }

            return new Weather();
        }

        /// <summary>
        /// Метод для получения координат
        /// </summary>
        /// <returns>Кортеж с широтой и долготой</returns>
        public (double, double) GetCoordinates()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Task<(double, double)> task = CoordinatesService.GetCoordinatesAsync(Name, Country);
                task.Wait();
                (double, double) coordinates = task.Result;
                return coordinates;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось получить координаты. Ошибка: {ex.Message}");
            }

            return (0, 0);
        }
        /// <summary>
        /// Метод для обратного геокодинга: получения типа объекта и адреса
        /// </summary>
        /// <returns>Строку с типом объекта и адресом</returns>
        public string GetAddress()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Task<string> task = CoordinatesService.ReverseGeocodeAsync(Latitude, Longitude);
                task.Wait();
                string address = task.Result;
                return address;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось получить адрес. Ошибка: {ex.Message}");
            }

            return "";
        }
    }
}