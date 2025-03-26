namespace Ramazanova_project_3_2.ForMenu
{
    /// <summary>
    /// Интерфейс для элементов меню.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Заголовок элемента меню.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Задача, которую выполняет элемент меню.
        /// </summary>
        public void Select();
    }
}