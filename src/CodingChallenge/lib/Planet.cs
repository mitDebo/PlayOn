using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChallenge.Lib
{
    public class Planet
    {
        [JsonProperty("name")]        
        public string Name {get; set;}

        [JsonProperty("diameter")]        
        public string Diameter {get; set;}

        [JsonProperty("rotation_period")]        
        public string RotationPeriod {get; set;}

        [JsonProperty("orbital_period")]        
        public string OrbitalPeriod {get; set;}

        [JsonProperty("gravity")]        
        public string Gravity {get; set;}

        [JsonProperty("population")]        
        public string Population {get; set;}

        [JsonProperty("climate")]        
        public string Climate {get; set;}

        [JsonProperty("terrain")]        
        public string Terrain {get; set;}

        [JsonProperty("surface_water")]        
        public string SurfaceWater {get; set;}

        [JsonProperty("residents")]        
        public List<string> Residents {get; set;}

        [JsonProperty("films")]        
        public List<string> Films {get; set;}

        [JsonProperty("url")]        
        public string Url {get; set;}

        [JsonProperty("created")]        
        public string Created {get; set;}

        [JsonProperty("edited")]        
        public string Edited {get; set;}
    }
}