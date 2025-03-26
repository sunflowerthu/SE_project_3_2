// Рамазанова Зарина Робертовна, гр. БПИ-249, вариант 5

using Ramazanova_project_3_2.CityDataDirectory;
using Ramazanova_project_3_2.ForMenu;
using Ramazanova_project_3_2.MainMenuItems;

namespace Ramazanova_project_3_2
{
    /// <summary>
    /// Класс с точкой входа в программу.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        public static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            while (true)
            {
                if (CityData.Cities.Any())
                {
                    break;
                }

                Console.WriteLine("Введите данные о городах.");
                new EnterDataMenuItem().Select();
            }
            List<IMenuItem> mainMenuItems =
            [
                new EnterDataMenuItem(),
                new TablesOfCitiesMenuItem(),
                new TextMapMenuItem(),
                new AddNewCityMenuItem(),
                new EditCityMenuItem(),
                new DeleteCityMenuItem(),
                new SaveToFileMenuItem(false),
                new ExitMenuItem()
            ];
            Menu menu = new(mainMenuItems);
            menu.Display();
        }
    }
}