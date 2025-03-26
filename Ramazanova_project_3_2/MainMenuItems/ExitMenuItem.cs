using Ramazanova_project_3_2.ForMenu;

namespace Ramazanova_project_3_2.MainMenuItems
{
    /// <summary>
    /// Выход из программы
    /// </summary>
    public class ExitMenuItem : IMenuItem
    {
        /// <summary>
        /// Заголовок метода.
        /// </summary>
        public string Title => "Выйти из программы";
        /// <summary>
        /// Задача, которую выполняет элемент меню.
        /// </summary>
        public void Select()
        {
            new SaveToFileMenuItem(true).Select();
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }
    }
}