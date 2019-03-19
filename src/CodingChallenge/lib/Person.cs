using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChallenge.Lib
{
    public class Person
    {
        [JsonProperty("name")]
        public string Name {get; set;}

        [JsonProperty("birth_year")]
        public string BirthYear {get; set;}

        [JsonProperty("eye_color")]
        public string EyeColor {get; set;}

        [JsonProperty("gender")]
        public string Gender {get; set;}

        [JsonProperty("hair_color")]
        public string HairColor {get; set;}

        [JsonProperty("height")]
        public string Height {get; set;}

        [JsonProperty("mass")]
        public string Mass {get; set;}

        [JsonProperty("skin_color")]
        public string SkinColor{get; set;}

        [JsonProperty("homeworld")]
        public string Homeworld {get; set;}

        [JsonProperty("films")]
        public List<string> Films {get; set;}

        [JsonProperty("species")]
        public List<string> Species {get; set;}

        [JsonProperty("nastarshipsme")]
        public List<string> Starships {get; set;}

        [JsonProperty("vechicles")]
        public List<string> Vechicles {get; set;}

        [JsonProperty("url")]
        public string Url {get; set;}

        [JsonProperty("created")]
        public string Created {get; set;}

        [JsonProperty("edited")]
        public string Edited {get; set;}
    }
}