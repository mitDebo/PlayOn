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

        public List<Person> GetAllPeople()
        {
            List<Person> allPeople = new List<Person>();
            
            PagenatedResult<Person> currentPage = GetPagenatedResult<Person>("people");
            allPeople.AddRange(currentPage.Results);
            while (currentPage.Next != null)
            {
                string nextUrl = currentPage.Next;
                currentPage = GetPagenatedResult<Person>("people", nextUrl.Substring(nextUrl.IndexOf(PAGE_QUERY_PARAM) + PAGE_QUERY_PARAM.Length));
                allPeople.AddRange(currentPage.Results);
            }
            
            return allPeople;
        }

        public List<Planet> GetAllPlanets()
        {
            List<Planet> allPlanets = new List<Planet>();
            
            string endpoint = "planets";

            PagenatedResult<Planet> currentPage = GetPagenatedResult<Planet>(endpoint);
            allPlanets.AddRange(currentPage.Results);
            while (currentPage.Next != null)
            {
                string nextUrl = currentPage.Next;
                currentPage = GetPagenatedResult<Planet>(endpoint, nextUrl.Substring(nextUrl.IndexOf(PAGE_QUERY_PARAM) + PAGE_QUERY_PARAM.Length));
                allPlanets.AddRange(currentPage.Results);
            }
            
            return allPlanets;
        }

        private PagenatedResult<T> GetPagenatedResult<T>(string endpoint, string pageNum = "1")
        {
            IRestRequest request = new RestRequest(endpoint);
            request.AddParameter("page", pageNum);

            IRestResponse response = Client.Get(request);
            PagenatedResult<T> currentPage;
            
            if (response.IsSuccessful)
            {
                currentPage = JsonConvert.DeserializeObject<PagenatedResult<T>>(response.Content);
            } 
            else 
            {
                throw new Exception("Error reading from the API");
            }

            return currentPage;
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