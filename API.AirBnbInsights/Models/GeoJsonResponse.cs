using System;
using Newtonsoft.Json;

namespace API.AirBnbInsights.Models
{
    public class GeoJsonResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        // Add any additional properties as needed
    }

    public class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class Properties
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("neighbourhood")]
        public string? Neighbourhood { get; set; }
    }
}

