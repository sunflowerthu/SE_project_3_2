using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.FileHelpers;
using Ramazanova_project_3_2.ForMenu;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню для сохранения данных в файл.
    /// </summary>
    public class SaveToFileMenuItem(bool isEnd) : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Сохранить данные в файл.";
        /// <summary>
        /// Значение для формата - JSON или CSV
        /// </summary>
        private bool _isJson;
        /// <summary>
        /// Значение для сохранения файла по желанию пользователя или в изначальный файл.
        /// </summary>
        private readonly bool _isEnd = isEnd;
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private string _personalPath = "";

        /// <summary>
        /// Задача, которую выполняет элемент меню.
        /// </summary>
        public void Select()
        {
            try
            {
                if (_isEnd)
                {
                    _personalPath = FileManager.GetCurrentDirectory();
                    SetFormat(_personalPath);
                }
                else
                {
                    Choose();
                    SetPath();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                if (_isJson)
                {
                    FileManager.WriteToJsonFile(_personalPath, CityData.Cities);
                }
                else
                {
                    FileManager.WriteToCsvFile(_personalPath, CityData.Cities);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Не удалось вывести данные в файл.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Метод для выбора формата
        /// </summary>
        private void Choose()
        {
            do
            {
                Console.WriteLine("Введите желаемый формат данных:" + "\n1. JSON" + "\n2. CSV");
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number == 1)
                    {
                        _isJson = true;
                    }
                    else if (number == 2)
                    {
                        _isJson = false;
                    }
                    else
                    {
                        Console.WriteLine("Введите корректный номер.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Неизвестный номер. Попробуйте еще раз.");
            } while (true);
        }

        private void SetPath()
        {
            Console.WriteLine("Введите путь к файлу:");
            _personalPath = Console.ReadLine() ?? throw new Exception("Неверный путь.");
        }

        private void SetFormat(string path)
        {
            if (path[^4..] == "json")
            {
                _isJson = true;
            }
            else if (path[^3..] == "csv")
            {
                _isJson = false;
            }
            else
            {
                throw new Exception("Некорректный путь");
            }
        }
    }
}