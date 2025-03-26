using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;
using Spectre.Console;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню для добавления нового города
    /// </summary>
    public class AddNewCityMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Добавить новый город.";
        
        /// <summary>
        /// Задача, которую выполняет пункт меню.
        /// </summary>
        public void Select()
        {
            string name;
            
            while (true)
            {
                name = AnsiConsole.Ask<string>("Введите название города:");
                if (string.IsNullOrWhiteSpace(name))
                {
                    AnsiConsole.MarkupLine("[red]Название города не может быть пустым.[/]");
                    continue;
                }
                break;
            }

            string country;

            while (true)
            {
                country = AnsiConsole.Ask<string>("Введите страну:");
                if (string.IsNullOrWhiteSpace(country))
                {
                    AnsiConsole.MarkupLine("[red]Название страны не может быть пустым.[/]");
                    continue;
                }
                break;
            }

            int population;
                
            while (true)
            {
                population = AnsiConsole.Ask<int>("Введите население (опционально, введите 0, чтобы пропустить):");
                if (population < 0)
                {
                    AnsiConsole.MarkupLine("[red]Население не может быть отрицательным.[/]");
                    continue;
                }

                break;
            }
            
            City city = new (name, country, 0, 0, population);

            (double, double) cort = GetCoordinates(city);
            city.Latitude = cort.Item1;
            city.Longitude = cort.Item2;
            
            CityData.Cities.Add(city);
            AnsiConsole.MarkupLine("[green]Город успешно добавлен.[/]");
        }
        
        /// <summary>
        /// Метод для получения координат в зависимости от выбора пользователя.
        /// </summary>
        /// <param name="city">Город</param>
        /// <returns>Кортеж с широтой и долготой</returns>
        private (double, double) GetCoordinates(City city)
        {
            (double, double) coords = city.GetCoordinates();
            double latitude = coords.Item1;
            double longitude = coords.Item2;
            
            Console.WriteLine($"Найденные координаты: {latitude}, {longitude}");
            
            bool confirmation = AnsiConsole.Prompt(
                new TextPrompt<bool>("Установить найденные координаты?")
                    .AddChoice(true)
                    .AddChoice(false)
                    .WithConverter(choice => choice ? "Да" : "Нет"));

            if (!confirmation)
            {
                while (true)
                {
                    latitude = AnsiConsole.Ask<double>("Введите широту:");
                    if (latitude is < -90 or > 90)
                    {
                        AnsiConsole.MarkupLine("[red]Широта должна быть в диапазоне от -90 до 90.[/]");
                        continue;
                    }
                    break;
                }
                
                while (true)
                {
                    longitude = AnsiConsole.Ask<double>("Введите долготу:");
                    if (longitude is < -180 or > 180)
                    {
                        AnsiConsole.MarkupLine("[red]Долгота должна быть в диапазоне от -180 до 180.[/]");
                        continue;
                    }

                    break;
                }
            }
            return (latitude, longitude);
        }
    }
}