using System.Globalization;
using System.Security;
using CsvHelper;
using Newtonsoft.Json;
using Ramazanova_project_3_2.CityDataDirectory;

namespace Ramazanova_project_3_2.FileHelpers
{
    /// <summary>
    /// Класс, отвечающий за input-output. 
    /// </summary>
    public abstract class FileManager
    {
        private static string _currentDirectory = "";
        /// <summary>
        /// Конструктор для этого класса.
        /// </summary>
        protected FileManager()
        {
            GetPath();
        }

        /// <summary>
        /// Метод для получения рабочей папки с файлами. 
        /// </summary>
        /// <returns> Путь</returns>
        public static string GetPath()
        {
            string[] directories = Directory.GetCurrentDirectory().Split(Path.DirectorySeparatorChar);
            return string.Join(Path.DirectorySeparatorChar, directories, 0, directories.Length - 3)
                   + Path.DirectorySeparatorChar + "Files" + Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Метод, необходимый для чтения строк из файла
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        /// <returns> Массив строк из файла </returns>
        public static string[] ReadFileLines(string path)
        {
            string[] text;
            try
            {
                text = File.ReadAllLines(path);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException)
            {
                throw new IOException("Файл не был найден", ex);
            }

            return text;
        }
        /// <summary>
        /// Метод для чтения файла единой строкой.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns></returns>
        /// <exception cref="IOException">Выкидывается при невозможности доступа к файлу.</exception>
        /// <exception cref="ArgumentException">Выкидывается, если путь к файлу некорректен.</exception>
        public static string ReadFile(string path)
        {
            string text;
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException)
            {
                throw new IOException("Не удалось прочитать файл.");
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                throw new ArgumentException("Некорректный путь.");
            }

            return text;
        }
        /// <summary>
        /// Метод для записи в файл csv формата. 
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="cities">Лист с данными о городах</param>
        /// <exception cref="ArgumentException">Выкидывается при некорректном пути.</exception>
        /// <exception cref="Exception">Выкидывается если возникает ошибка при сериализации </exception>
        public static void WriteToCsvFile(string path, List<City> cities)
        {
            try
            {
                using (StreamWriter writer = new(path))
                using (CsvWriter csv = new(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(cities);
                }

                Console.WriteLine("Данные успешно записаны в CSV-файл.");
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                throw new ArgumentException("Некорректный путь.");
            }
            catch (Exception e) when (e is CsvHelperException or IOException)
            {
                throw new ("Не удалось записать данные.");
            }
        }

        /// <summary>
        /// Метод для записи в файл json формата. 
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="cities">Лист с данными о городах</param>
        /// <exception cref="ArgumentException">Выкидывается при некорректном пути.</exception>
        /// <exception cref="Exception">Выкидывается если возникает ошибка при сериализации </exception>
        public static void WriteToJsonFile(string path, List<City> cities)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(cities, Formatting.Indented);

                File.WriteAllText(path, jsonData);

                Console.WriteLine("Данные успешно записаны в JSON-файл.");
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                throw new ArgumentException("Некорректный путь.");
            }
            catch (Exception ex) when (ex is JsonSerializationException or IOException)
            {
                throw new ("Не удалось записать данные.");
            }
        }

        /// <summary>
        /// Путь для установления текущей директории
        /// </summary>
        /// <param name="path">Путь</param>
        public static void SetCurrentDirectory(string path)
        {
            _currentDirectory = path;
        }
        /// <summary>
        /// Метод для получения текущей диерктории.
        /// </summary>
        /// <returns>Текущая директория</returns>
        public static string GetCurrentDirectory()
        {
            return _currentDirectory;
        }
    }
}