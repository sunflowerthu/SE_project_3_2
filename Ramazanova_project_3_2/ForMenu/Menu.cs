namespace Ramazanova_project_3_2.ForMenu
{
    /// <summary>
    /// Класс для меню.
    /// </summary>
    public class Menu
    {
        private readonly List<IMenuItem> _menuItems;
        private int _selectedIndex;

        /// <summary>
        /// Конструктор для меню
        /// </summary>
        /// <param name="menuItems"> Элементы меню</param>
        public Menu(List<IMenuItem> menuItems)
        {
            _menuItems = menuItems;
            _selectedIndex = 0;
        }

        /// <summary>
        /// Метод, выводящий меню
        /// </summary>
        public void Display()
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                for (int i = 0; i < _menuItems.Count; i++)
                {
                    if (i == _selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"V(=^･ω･^=)v  > {i+1}. {_menuItems[i].Title} <");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i+1}. {_menuItems[i].Title}");
                    }
                }

                key = Console.ReadKey(true).Key;
                HandleKeyPress(key);
            } while (key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Обработчик нажатий на кнопки.
        /// </summary>
        /// <param name="key"> Клавиша, нажимаемая пользователем. </param>
        private void HandleKeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _selectedIndex = _selectedIndex == 0 ? _menuItems.Count - 1 : _selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = _selectedIndex == _menuItems.Count - 1 ? 0 : _selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    _menuItems[_selectedIndex].Select();
                    Console.WriteLine("\nНажмите любую клавишу, что перейти в меню");
                    Console.ReadKey();
                    break;
            }
        }
    }
}