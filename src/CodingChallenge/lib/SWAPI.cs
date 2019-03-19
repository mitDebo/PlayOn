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
}