using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ramazanova_project_3_2.ApiHepler;

namespace Ramazanova_project_3_2.WeatherDirectory
{
    /// <summary>
    /// Класс для работы с API
    /// </summary>
    public static class WeatherService
    {
        /// <summary>
        /// Ключ для апи.
        /// </summary>
        private const string ApiKey = "884819e12b4567032b5be88665c2ef00";

        /// <summary>
        /// Метод для получения погоды
        /// </summary>
        /// <param name="city">Город</param>
        /// <returns>Таск с погодой</returns>
        public static async Task<Weather> GetWeatherAsync(string city)
        {
            try
            {
                JObject weatherData = await GetWeatherDataAsync(city);
                return ParseWeather(weatherData);
            }
            catch (Exception ex) when (ex is HttpRequestException or JsonReaderException)
            {
                Console.WriteLine($"Ошибка при запросе к API: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            return new Weather();
        }
        
        /// <summary>
        /// Метод для получения данных о погоде из апи.
        /// </summary>
        /// <param name="city">Город</param>
        /// <returns>Таск с объектом парсинга</returns>
        private static async Task<JObject> GetWeatherDataAsync(string city)
        {
            string url =
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric&lang=ru";

            try
            {
                string responseData = await ApiRequest.GetResponseData(url);
                return JObject.Parse(responseData);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Некорректный формат JSONю");
                throw;
            }
        }

        /// <summary>
        /// Метод для парсинга JSON с погодой
        /// </summary>
        /// <param name="weatherData">Объект для париснга</param>
        /// <returns>Объект погода</returns>
        private static Weather ParseWeather(JObject weatherData)
        {
            try
            {
                string weatherDescription = weatherData["weather"]?[0]?["description"]?.ToString() ?? "Нет данных";
                int temperature = weatherData["main"]?["temp"]?.ToObject<int>() ?? 0;
                int feelsLike = weatherData["main"]?["feels_like"]?.ToObject<int>() ?? 0;
                int humidity = weatherData["main"]?["humidity"]?.ToObject<int>() ?? 0;
                int cloudiness = weatherData["clouds"]?["all"]?.ToObject<int>() ?? 0;
                double windSpeed = weatherData["wind"]?["speed"]?.ToObject<double>() ?? 0;

                double precipitation = 0;
                if (weatherData["rain"] != null)
                {
                    precipitation = weatherData["rain"]?["1h"]?.ToObject<double>() ?? 0; 
                }
                else if (weatherData["snow"] != null)
                {
                    precipitation = weatherData["snow"]?["1h"]?.ToObject<double>() ?? 0; 
                }

                return new Weather(weatherDescription, temperature, feelsLike, humidity, cloudiness, windSpeed,
                    precipitation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке данных о погоде: {ex.Message}");
                return new Weather();
            }
        }
    }
}