using CsvHelper.Configuration;
using Ramazanova_project_3_2.CityDataDirectory;

namespace Ramazanova_project_3_2.FileHelpers
{
    /// <summary>
    /// Маппинг для City
    /// </summary>
    public sealed class CityMap : ClassMap<City>
    {
        /// <summary>
        /// Конструктор для маппинга.
        /// </summary>
        public CityMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Country).Name("Country");
            Map(m => m.Population).Name("Population").Optional();
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Longitude).Name("Longitude");
        }
    }
}