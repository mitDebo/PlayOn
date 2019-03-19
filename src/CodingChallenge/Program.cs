using System;
using System.Collections.Generic;
using RestSharp;
using CodingChallenge.Lib;

namespace CodingChallenge
{
    public class Program
    {
        static List<Person> People { get; set; }
        static List<Planet> Planets { get; set; }

        public static void Main(string[] args)
        {
            if (args.Length > 1)
                Console.WriteLine("Please either provide no parameters, or the name of a Star Wars planet");
            else
            {
                GetInfoFromSWAPI();
                if (args.Length == 1)
                    PrintOutPlanetInhabitants(args[0]);
                else
                    PrintOutAllPlanetsAndPeople();
            }

            Console.WriteLine("Done");
        }

        static void GetInfoFromSWAPI()
        {
            SWAPI swapi = new SWAPI();
            People = swapi.GetAllPeople();
            Planets = swapi.GetAllPlanets();
        }

        static void PrintOutPlanetInhabitants(String planetName)
        {

        }

        static void PrintOutAllPlanetsAndPeople()
        {

        }
    }
}
