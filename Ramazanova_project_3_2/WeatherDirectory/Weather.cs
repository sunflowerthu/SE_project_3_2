namespace Ramazanova_project_3_2.WeatherDirectory
{
    /// <summary>
    /// Класс для погоды
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// Описание погоды
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature { get; }
        /// <summary>
        /// Температура по ощущению
        /// </summary>
        public int FeelsLike { get; }
        /// <summary>
        /// Влажность в процентах
        /// </summary>
        public int Humidity { get; }
        /// <summary>
        /// Облачность в процентах
        /// </summary>
        public int Cloudiness { get; }
        /// <summary>
        /// Скорость ветра в м/с
        /// </summary>
        public double WindSpeed { get; }
        /// <summary>
        /// Осадки в мм
        /// </summary>
        public double Precipitation { get; } 

        /// <summary>
        /// Конструктор для погоды 
        /// </summary>
        /// <param name="description">Описание погоды</param>
        /// <param name="temperature">Температура</param>
        /// <param name="feelsLike">Ощущаемая температура</param>
        /// <param name="humidity">Влажность</param>
        /// <param name="cloudiness">Облачность</param>
        /// <param name="windSpeed">Скорость ветра</param>
        /// <param name="precipitation">Осадки</param>
        public Weather(string description, double temperature, int feelsLike, int humidity, int cloudiness, double windSpeed, double precipitation)
        {
            Description = description;
            Temperature = temperature;
            FeelsLike = feelsLike;
            Humidity = humidity;
            Cloudiness = cloudiness;
            WindSpeed = windSpeed;
            Precipitation = precipitation;
        }

        /// <summary>
        /// Пустой конструктор для неудачного парсинга
        /// </summary>
        public Weather() { }
    }
}