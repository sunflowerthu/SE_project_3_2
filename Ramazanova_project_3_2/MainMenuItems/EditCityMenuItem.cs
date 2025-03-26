using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;
using Spectre.Console;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню для редактирования данных о городе.
    /// </summary>
    public class EditCityMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Отредактировать данные о городе.";

        /// <summary>
        /// Задача, которую выполняет пункт меню.
        /// </summary>
        public void Select()
        {
            if (!CityData.Cities.Any())
            {
                Console.WriteLine("Введите данные о городах.");
                return;
            }
            Console.WriteLine("Введите название города, данные о котором Вы хотите изменить:");
            string cityName = AnsiConsole.Ask<string>("Введите название города для редактирования:");
            City? city =
                CityData.Cities.FirstOrDefault(c => c.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));
            if (city == null)
            {
                AnsiConsole.MarkupLine("[red]Город не найден.[/]");
                return;
            }

            AnsiConsole.MarkupLine("[yellow]Введите новые данные (нажмите Enter, чтобы оставить текущее значение):[/]");

            city.Name = AnsiConsole.Ask("Новое название города:", city.Name);

            city.Country = AnsiConsole.Ask("Новая страна:", city.Country);

            int population;

            while (true)
            {
                population = AnsiConsole.Ask("Введите население (опционально, введите 0, чтобы пропустить):",
                    city.Population);
                if (population < 0)
                {
                    AnsiConsole.MarkupLine("[red]Население не может быть отрицательным.[/]");
                    continue;
                }

                break;
            }

            city.Population = population;

            double longitude;
            while (true)
            {
                longitude = AnsiConsole.Ask("Введите долготу:", city.Longitude);
                if (longitude is < -180 or > 180)
                {
                    AnsiConsole.MarkupLine("[red]Долгота должна быть в диапазоне от -180 до 180.[/]");
                    continue;
                }

                break;
            }

            city.Longitude = longitude;

            double latitude;
            while (true)
            {
                latitude = AnsiConsole.Ask("Введите широту:", city.Latitude);
                if (latitude is < -90 or > 90)
                {
                    AnsiConsole.MarkupLine("[red]Широта должна быть в диапазоне от -90 до 90.[/]");
                    continue;
                }

                break;
            }

            city.Latitude = latitude;

            AnsiConsole.MarkupLine("[green]Город успешно отредактирован.[/]");
        }
    }
}