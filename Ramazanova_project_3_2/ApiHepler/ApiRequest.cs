namespace Ramazanova_project_3_2.ApiHepler
{
    /// <summary>
    /// Класс для запросов к апи.
    /// </summary>
    public static class ApiRequest
    {
        /// <summary>
        /// Метод для получения строки из OpenWeatherMap API
        /// </summary>
        /// <param name="url">URL для запроса</param>
        /// <returns>Таск со строкой json</returns>
        public static async Task<string> GetResponseData(string url)
        {
            using HttpClient client = new();

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();

            return responseData;
        }

        /// <summary>
        /// Метод для получения строки из Nominatim API
        /// </summary>
        /// <param name="url">URL для запроса</param>
        /// <returns>Таск со строкой json</returns>
        public static async Task<string> GetNominatimResponseData(string url)
        {
            using HttpClient client = new();

            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36";

            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            await Task.Delay(1000);
            
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }
}