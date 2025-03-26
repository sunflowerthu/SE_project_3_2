using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.FileHelpers;
using Ramazanova_project_3_2.ForMenu;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню для ввода данных.
    /// </summary>
    public class EnterDataMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Ввести путь к файлу";
        /// <summary>
        /// Значение формата файла вывода - JSON или CSV
        /// </summary>
        private bool _isJson;
        /// <summary>
        /// Пользовательский путь к файлу.
        /// </summary>
        private string _personalPath = "";
        /// <summary>
        /// Путь к файлу с заранее подготовленными данными.
        /// </summary>
        private readonly string? _basicPath = FileManager.GetPath() + "Input" + Path.DirectorySeparatorChar + "CityData.";
        /// <summary>
        /// Задача, которую выполняет элемент меню.
        /// </summary>
        public void Select()
        {
            do
            {
                Console.WriteLine("Введите желаемый формат данных:" + "\n1. JSON" + "\n2. CSV");
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        case 1:
                            _isJson = true;
                            break;
                        case 2:
                            _isJson = false;
                            break;
                        default:
                            Console.WriteLine("Введите корректный номер.");
                            continue;
                    }

                    break;
                }
                Console.WriteLine("Неизвестный номер. Попробуйте еще раз.");
            } while (true);
            
            Console.WriteLine("Введите путь к файлу или введите 1, если хотите взять данные из исходного файла.");
            bool isOk = false;
            do
            {
                try
                {
                    _personalPath = Console.ReadLine() ?? throw new Exception("Неверный путь.");
                    if (_personalPath == "1")
                    {
                        _personalPath = _isJson ? _basicPath + "json" : _basicPath + "csv";
                    }

                    DataParser parser = new();
                    List<City> newCities = _isJson
                        ? parser.ParseJsonFile(_personalPath)
                        : parser.ParseCsvFile(_personalPath);
                    if (newCities.Count > 0)
                    {
                        CityData.SetCities(newCities);
                        isOk = true;
                    }
                    else
                    {
                        Console.WriteLine("Данные о городах отсутствуют.");
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Произошла ошибка с файлом. Введите ещё раз");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            } while (!isOk);
            
            FileManager.SetCurrentDirectory(_personalPath);
        }
    }
}