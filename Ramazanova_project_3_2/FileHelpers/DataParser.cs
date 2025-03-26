using System.Globalization;
using CsvHelper;
using Newtonsoft.Json;
using Ramazanova_project_3_2.CityDataDirectory;

namespace Ramazanova_project_3_2.FileHelpers
{
    /// <summary>
    /// Класс для парсинга данных.
    /// </summary>
    public class DataParser
    {
        /// <summary>
        /// Метод для получения строк файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Строки файла</returns>
        /// <exception cref="FileNotFoundException"></exception>
        private string[] GetFileLines(string path)
        {
            string[] fileLines;
            try
            {
                fileLines = FileManager.ReadFileLines(path);
            }
            catch
            {
                throw new FileNotFoundException();
            }

            return fileLines;
        }
        /// <summary>
        /// Метод для парсинга csv-файла
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns>Распарсенные данные.</returns>
        public List<City> ParseCsvFile(string path)
        {
            string[] fileLines = GetFileLines(path);
            
            List<City> cities = new();

            try
            {
                using StringReader reader = new(string.Join(Environment.NewLine, fileLines));
                using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<CityMap>();
                cities = csv.GetRecords<City>().ToList();
            }
            catch (Exception e) when (e is ArgumentNullException or DirectoryNotFoundException)
            {
                Console.WriteLine("Не удалось распарсить файл.");
            }
            catch (CsvHelperException)
            {
                Console.WriteLine("Некорректный формат файла.");
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                Console.WriteLine("Не удалось найти файл.");
            }
            cities = ValidateCities(cities);

            return cities;
        }
        /// <summary>
        /// Метод для парсинга json-файла
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns>Распарсенные данные.</returns>
        /// <exception cref="InvalidOperationException">Выкидывается, если в cities некорректное представление. </exception>
        public List<City> ParseJsonFile(string path)
        {
            string jsonData = FileManager.ReadFile(path);

            List<City> cities = new();
            try
            {
                cities = JsonConvert.DeserializeObject<List<City>>(jsonData) ?? throw new InvalidOperationException();
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                Console.WriteLine("Не удалось найти файл.");
            }
            catch (Exception e) when (e is JsonReaderException || e is JsonSerializationException)
            {
                Console.WriteLine("Не удалось распарсить файл.");
            }

            cities = ValidateCities(cities);
            
            return cities;
        }

        /// <summary>
        /// Метод для валидации данных о городах.
        /// </summary>
        /// <param name="cities">Данные о городах.</param>
        /// <returns>Список с данными о городах.</returns>
        private List<City> ValidateCities(List<City> cities)
        {
            List<City> validCities = [];
            foreach (City city in cities)
            {
                if (city.Latitude is >= -90 and <= 90 &&
                    city.Longitude is >= -180 and <= 180)
                {
                    validCities.Add(city);
                }
            }

            return validCities;
        }
    }
}