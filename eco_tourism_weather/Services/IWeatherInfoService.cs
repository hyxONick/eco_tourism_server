using Microsoft.EntityFrameworkCore;
using eco_tourism_weather.Models;
using eco_tourism_weather.DB;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace eco_tourism_weather.Services
{
    /// <summary>
    /// Interface for weather information services.
    /// </summary>
    public interface IWeatherInfoService
    {
        /// <summary>
        /// Retrieves weather information for a specified city.
        /// If data for today already exists in the database, it returns the data from the database.
        /// Otherwise, it fetches real-time weather data from the API and stores it in the database.
        /// </summary>
        /// <param name="location">City name or location identifier</param>
        /// <returns>Weather information entity object</returns>
        Task<WeatherInfo> GetWeatherAsync(string location);
    }

    /// <summary>
    /// Implementation of the IWeatherInfoService interface.
    /// </summary>
    public class WeatherInfoService : IWeatherInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly EcoTourismWeatherContext _context;

        public WeatherInfoService(HttpClient httpClient, EcoTourismWeatherContext context)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<WeatherInfo> GetWeatherAsync(string cityName)
        {
            var today = DateTime.Today;

            // Step 1: Check if the weather info for today already exists in the database
            var weatherInfo = await _context.WeatherInfos
                .Where(w => w.Name == cityName && w.Localtime.Date == today)
                .FirstOrDefaultAsync();

            if (weatherInfo != null)
            {
                // Return from database if data exists
                return weatherInfo;
            }

            // Step 2: Fetch weather data from the API if no data found in the database
            var apiUrl = $"http://api.weatherapi.com/v1/current.json?key=d5859c3f33bc4e69ac9134143241409&q={cityName}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch weather data from the API.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            // Step 3: Parse the JSON response into the Weather model
            var weatherApiResponse = JsonSerializer.Deserialize<ApiWeatherResponse>(responseBody);

            if (weatherApiResponse == null)
            {
                throw new Exception("Failed to parse weather data.");
            }

            // Step 4: Map the API response to the Weather model and save it to the database
            var newWeather = new WeatherInfo
            {
                Name = weatherApiResponse.Location.Name ?? throw new ArgumentNullException(nameof(weatherApiResponse.Location.Name)),
                Region = weatherApiResponse.Location.Region ?? "Unknown",
                Country = weatherApiResponse.Location.Country ?? "Unknown",
                Latitude = weatherApiResponse.Location.Lat,
                Longitude = weatherApiResponse.Location.Lon,
                TzId = weatherApiResponse.Location.TzId ?? "Unknown",
                LocaltimeEpoch = weatherApiResponse.Location.LocaltimeEpoch,
                Localtime = DateTime.Parse(weatherApiResponse.Location.Localtime ?? throw new ArgumentNullException(nameof(weatherApiResponse.Location.Localtime))),

                LastUpdatedEpoch = weatherApiResponse.Current.LastUpdatedEpoch,
                LastUpdated = DateTime.Parse(weatherApiResponse.Current.LastUpdated ?? throw new ArgumentNullException(nameof(weatherApiResponse.Current.LastUpdated))),
                TempC = weatherApiResponse.Current.TempC,
                TempF = weatherApiResponse.Current.TempF,
                IsDay = weatherApiResponse.Current.IsDay == 1,
                ConditionText = weatherApiResponse.Current.Condition.Text ?? "Unknown",
                ConditionIcon = weatherApiResponse.Current.Condition.Icon ?? "Unknown",
                ConditionCode = weatherApiResponse.Current.Condition.Code,

                WindMph = weatherApiResponse.Current.WindMph,
                WindKph = weatherApiResponse.Current.WindKph,
                WindDegree = weatherApiResponse.Current.WindDegree,
                PrecipMm = weatherApiResponse.Current.PrecipMm,
                Humidity = weatherApiResponse.Current.Humidity,
                Cloud = weatherApiResponse.Current.Cloud,
                FeelslikeC = weatherApiResponse.Current.FeelslikeC,
                FeelslikeF = weatherApiResponse.Current.FeelslikeF,
                WindchillC = weatherApiResponse.Current.WindchillC,
                WindchillF = weatherApiResponse.Current.WindchillF,
                HeatindexC = weatherApiResponse.Current.HeatindexC,
                VisKm = weatherApiResponse.Current.VisKm,
                UV = weatherApiResponse.Current.UV,
                GustMph = weatherApiResponse.Current.GustMph,
                GustKph = weatherApiResponse.Current.GustKph
            };

            // Step 5: Save the new weather data to the database
            _context.WeatherInfos.Add(newWeather);
            await _context.SaveChangesAsync();

            return newWeather;
        }
    }

    /// <summary>
    /// Represents the weather API response.
    /// </summary>
    public class ApiWeatherResponse
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; } = new Location();

        [JsonPropertyName("current")]
        public CurrentWeather Current { get; set; } = new CurrentWeather();
    }

    /// <summary>
    /// Represents location details in the weather API response.
    /// </summary>
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("lat")]
        public decimal Lat { get; set; }

        [JsonPropertyName("lon")]
        public decimal Lon { get; set; }

        [JsonPropertyName("tz_id")]
        public string? TzId { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public long LocaltimeEpoch { get; set; }

        [JsonPropertyName("localtime")]
        public string? Localtime { get; set; }
    }

    /// <summary>
    /// Represents current weather details in the weather API response.
    /// </summary>
    public class CurrentWeather
    {
        [JsonPropertyName("last_updated_epoch")]
        public long LastUpdatedEpoch { get; set; }

        [JsonPropertyName("last_updated")]
        public string? LastUpdated { get; set; }

        [JsonPropertyName("temp_c")]
        public decimal TempC { get; set; }

        [JsonPropertyName("temp_f")]
        public decimal TempF { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; } = new Condition();

        [JsonPropertyName("wind_mph")]
        public decimal WindMph { get; set; }

        [JsonPropertyName("wind_kph")]
        public decimal WindKph { get; set; }

        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }

        [JsonPropertyName("precip_mm")]
        public decimal PrecipMm { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }

        [JsonPropertyName("feelslike_c")]
        public decimal FeelslikeC { get; set; }

        [JsonPropertyName("feelslike_f")]
        public decimal FeelslikeF { get; set; }

        [JsonPropertyName("windchill_c")]
        public decimal WindchillC { get; set; }

        [JsonPropertyName("windchill_f")]
        public decimal WindchillF { get; set; }

        [JsonPropertyName("heatindex_c")]
        public decimal HeatindexC { get; set; }

        [JsonPropertyName("vis_km")]
        public decimal VisKm { get; set; }

        [JsonPropertyName("uv")]
        public decimal UV { get; set; }

        [JsonPropertyName("gust_mph")]
        public decimal GustMph { get; set; }

        [JsonPropertyName("gust_kph")]
        public decimal GustKph { get; set; }
    }

    /// <summary>
    /// Represents the condition details in the current weather.
    /// </summary>
    public class Condition
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
