using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;
using Spectre.Console;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Пункт меню для удаления города из данных.
    /// </summary>
    public class DeleteCityMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок пункта меню.
        /// </summary>
        public string Title => "Удалить данные о городе.";
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
            string cityName = AnsiConsole.Ask<string>("Введите название города для удаления:");
            City? cityToRemove = CityData.Cities.FirstOrDefault(c => c.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));

            if (cityToRemove == null)
            {
                AnsiConsole.MarkupLine("[red]Город не найден.[/]");
                return;
            }
            
            if (AnsiConsole.Prompt(
                    new TextPrompt<bool>($"Вы уверены, что хотите удалить город {cityToRemove.Name}")
                        .AddChoice(true)
                        .AddChoice(false)
                        .WithConverter(choice => choice ? "Да" : "Нет")))
            {
                CityData.Cities.Remove(cityToRemove);
                AnsiConsole.MarkupLine("[green]Город успешно удален.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Удаление отменено.[/]");
            }
        }
    }
}