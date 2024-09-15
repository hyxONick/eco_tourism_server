namespace eco_tourism_weather.Models
{
    public class WeatherInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;            // cityName
        public string Region { get; set; } = string.Empty;          // Region
        public string Country { get; set; } = string.Empty;         // Country
        public decimal Latitude { get; set; }                            // Latitude
        public decimal Longitude { get; set; }                            // Longitude
        public string TzId { get; set; } = string.Empty;            // time zone ID
        public long LocaltimeEpoch { get; set; }                    // Local time Unix time stamp
        public DateTime Localtime { get; set; }                     // Local time

        public long LastUpdatedEpoch { get; set; }                  // Last updated Unix timestamp
        public DateTime LastUpdated { get; set; }                   // Last updated
        public decimal TempC { get; set; }                          // Temperature (degrees Celsius)
        public decimal TempF { get; set; }                          // Temperature (degrees Fahrenheit)
        public bool IsDay { get; set; }                             // Whether it is daytime (true = yes, false = no)
        public string ConditionText { get; set; } = string.Empty;   // Description of weather conditions
        public string ConditionIcon { get; set; } = string.Empty;   // Weather icon URL
        public int ConditionCode { get; set; }                      // Weather code

        public decimal WindMph { get; set; }                        // 
        public decimal WindKph { get; set; }                        // Wind speed (MPH)
        public int WindDegree { get; set; }                         // Wind Angle
        public decimal PrecipMm { get; set; }                       // Precipitation (mm)
        public int Humidity { get; set; }                           // Humidity (%)
        public int Cloud { get; set; }                              // Cloud cover (%)
        public decimal FeelslikeC { get; set; }                     // Body temperature (degrees Celsius)
        public decimal FeelslikeF { get; set; }                     // Body temperature (Fahrenheit)
        public decimal WindchillC { get; set; }                     // Wind chill temperature (Celsius)
        public decimal WindchillF { get; set; }                     // Wind chill temperature (F)
        public decimal HeatindexC { get; set; }                     // Heat index (Celsius)
        public decimal VisKm { get; set; }                          // Visibility (km)
        public decimal UV { get; set; }                             // ultraviolet index
        public decimal GustMph { get; set; }                        // Wind gust speed (MPH)
        public decimal GustKph { get; set; }                        // Gust speed (km/h)
    }
}
