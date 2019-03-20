using RestSharp;
using RestSharp.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CodingChallenge.Interfaces;

namespace CodingChallenge.Lib
{
    public class SWAPI : ISWAPI
    {
        public IRestClient Client { 
            get; 
            internal set; 
        }
        const string PAGE_QUERY_PARAM = "?page=";

        public SWAPI()
        {
            Client = new RestClient("https://swapi.co/api");
        }

        /// <summary>
        /// Gets all the people from the Star Wars API
        /// </summary>
        public IList<Person> GetAllPeople()
        {
            List<Person> allPeople = new List<Person>();
            
            string endpoint = "people";

            PaginatedResult<Person> currentPage = GetPaginatedResult<Person>(endpoint);
            allPeople.AddRange(currentPage.Results);
            while (currentPage.Next != null)
            {
                string nextUrl = currentPage.Next;
                currentPage = GetPaginatedResult<Person>(endpoint, nextUrl.Substring(nextUrl.IndexOf(PAGE_QUERY_PARAM) + PAGE_QUERY_PARAM.Length));
                allPeople.AddRange(currentPage.Results);
            }
            
            return allPeople;
        }

        /// <summary>
        /// Gets all the planets from the Star Wars API
        /// </summary>
        public IList<Planet> GetAllPlanets()
        {
            List<Planet> allPlanets = new List<Planet>();
            
            string endpoint = "planets";

            PaginatedResult<Planet> currentPage = GetPaginatedResult<Planet>(endpoint);
            allPlanets.AddRange(currentPage.Results);
            while (currentPage.Next != null)
            {
                string nextUrl = currentPage.Next;
                currentPage = GetPaginatedResult<Planet>(endpoint, nextUrl.Substring(nextUrl.IndexOf(PAGE_QUERY_PARAM) + PAGE_QUERY_PARAM.Length));
                allPlanets.AddRange(currentPage.Results);
            }
            
            return allPlanets;
        }

        private PaginatedResult<T> GetPaginatedResult<T>(string endpoint, string pageNum = "1")
        {
            IRestRequest request = new RestRequest(endpoint);
            request.AddParameter("page", pageNum);

            IRestResponse response = Client.Execute(request);
            PaginatedResult<T> currentPage;
            
            if (response.IsSuccessful)
            {
                currentPage = JsonConvert.DeserializeObject<PaginatedResult<T>>(response.Content);
            } 
            else 
            {
                throw new Exception("Error reading from the API");
            }

            return currentPage;
        }
    }

    /// <summary>
    /// Simple encapsulating class to deal with pagination
    /// </summary>
    class PaginatedResult<T>
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