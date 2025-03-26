using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ramazanova_project_3_2.ApiHepler;

namespace Ramazanova_project_3_2.CoordinatesDirectory
{
    public static class CoordinatesService
    {
        public static async Task<string> ReverseGeocodeAsync(double latitude, double longitude)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?lat={latitude}&lon={longitude}&format=json";
            
            try
            {
                string jsonResponse = await ApiRequest.GetNominatimResponseData(url);

                JObject result = JObject.Parse(jsonResponse);

                string displayName = result["display_name"]?.ToString() ??
                                     throw new InvalidOperationException(
                                         "Свойство 'display_name' отсутствует или пустое.");
                string type = result["type"]?.ToString() ??
                              throw new InvalidOperationException("Свойство 'type' отсутствует или пустое.");

                return $"{type}; {displayName}";
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка при выполнении запроса к API Nominatim: {ex.Message}");
            }
            catch (JsonException)
            {
                throw new Exception("Ошибка при обработке JSON-ответа от API Nominatim.");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Ошибка при обработке данных от API Nominatim: {ex.Message}");
            }
            catch (Exception)
            {
                throw new Exception("Непредвиденная ошибка при выполнении обратного геокодирования.");
            }
        }


        public static async Task<(double, double)> GetCoordinatesAsync(string city, string country)
        {
            string url = $"https://nominatim.openstreetmap.org/search?format=json&city={city}&country={country}";
            
            try
            {
                string jsonResponse = await ApiRequest.GetNominatimResponseData(url);

                JArray results = JArray.Parse(jsonResponse);

                if (results.Count == 0)
                {
                    throw new InvalidOperationException("Массив результатов пуст.");
                }

                JObject firstResult = (JObject)results[0];

                string latStr = firstResult["lat"]?.ToString() ??
                                throw new InvalidOperationException("Свойство 'lat' отсутствует или пустое.");
                string lonStr = firstResult["lon"]?.ToString() ??
                                throw new InvalidOperationException("Свойство 'lon' отсутствует или пустое.");

                double lat = double.Parse(latStr);
                double lon = double.Parse(lonStr);

                return (lat, lon);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка при выполнении запроса к API Nominatim: {ex.Message}");
            }
            catch (JsonException)
            {
                throw new Exception("Ошибка при обработке JSON-ответа от API Nominatim.");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Ошибка при обработке данных от API Nominatim: {ex.Message}");
            }
            catch (FormatException)
            {
                throw new Exception("Ошибка при преобразовании координат в числа.");
            }
            catch (Exception)
            {
                throw new Exception("Непредвиденная ошибка при получении координат.");
            }
        }
    }
}
