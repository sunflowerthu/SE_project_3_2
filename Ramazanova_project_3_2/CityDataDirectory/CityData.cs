namespace Ramazanova_project_3_2.CityDataDirectory
{
    /// <summary>
    /// Класс для хранения данных о городах.
    /// </summary>
    public class CityData
    {
        /// <summary>
        /// Свойство для листа с городами.
        /// </summary>
        public static List<City> Cities
        {
            get;
            private set;
        } = new();
        /// <summary>
        /// Метод для установления списка с городами.
        /// </summary>
        /// <param name="cities"></param>
        public static void SetCities(List<City> cities)
        {
            Cities = cities;
        }
    }
}