using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodingChallenge.Lib
{
    public class SWAPI
    {
        IRestClient client;

        public SWAPI()
        {
            client = new RestClient("https://swapi.co");
        }

        public List<Person> GetAllPeople()
        {
            IRestRequest request = new RestRequest("people");
            return new List<Person>();
        }

        public List<Planet> GetAllPlanets()
        {
            IRestRequest request = new RestRequest("planets");

            return new List<Planet>();
        }
    }

    public class PagenatedResult<T>
    {
        [JsonProperty("count")]
        public int Count {get; set;}

        [JsonProperty("next")]
        public string Next {get; set;}

        [JsonProperty("previous")]
        public string Previous {get;set;}

        [JsonProperty("results")]
        public List<T> Results {get; set;}
    }
}