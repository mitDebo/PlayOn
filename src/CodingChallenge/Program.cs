using System;
using System.Collections.Generic;
using RestSharp;
using CodingChallenge.Interfaces;
using CodingChallenge.Lib;

namespace CodingChallenge
{
    public class Program
    {
        public IList<Person> People { get; private set; }
        public IList<Planet> Planets { get; private set; }
        public IDictionary<string, Population> InhabitantLookup { get; private set; }
        public ISWAPI SWAPI { get; internal set; }

        public Program()
        {
            SWAPI = new SWAPI();
        }

        public void Init()
        {
            GetInfoFromSWAPI();            
            PopulateLookupTables();
        }

        public static void Main(string[] args)
        {
            if (args.Length > 1)
                Console.WriteLine("Please either provide no parameters, or the name of a Star Wars planet");
            else
            {
                Program p = new Program();
                p.Init();
                if (args.Length == 1)
                    p.PrintOutPlanetInhabitants(args[0]);
                else
                    p.PrintOutAllPlanetsAndPeople();
            }
        }

        /// <summary>
        /// Prints a list of all people and planets from the Star Wars API
        /// </summary>
        public void PrintOutAllPlanetsAndPeople()
        {
            PrintAllPeople();
            PrintAllPlanets();
        }

        /// <summary>
        /// Given a planet, prints out everyone associated with that planet
        /// </summary>
        void PrintOutPlanetInhabitants(string planetName)
        {
            if (!InhabitantLookup.ContainsKey(planetName.ToLower()))
            {
                Console.WriteLine("{0} does not exist in Star Wars", planetName);
                return;
            }

            Population pop = InhabitantLookup[planetName.ToLower()];
            pop.PrintAll();
            
        }

        void GetInfoFromSWAPI()
        {
            People = SWAPI.GetAllPeople();
            Planets = SWAPI.GetAllPlanets();
        }        

        void PrintAllPeople()
        {
            Console.WriteLine("PEOPLE ({0}):", People.Count);
            foreach (Person p in People)
                Console.WriteLine("\t{0}", p.Name);
        }

        void PrintAllPlanets()
        {
            Console.WriteLine("PLANETS ({0}):", Planets.Count);
            foreach (Planet p in Planets)
                Console.WriteLine("\t{0}", p.Name);
        }

        void PopulateLookupTables()
        {
            InhabitantLookup = new Dictionary<string, Population>();
            IDictionary<string, Planet> planetUrlLookup = CreatePlanetUrlLookup();
            IDictionary<string, Person> personUrlLookup = CreatePersonUrlLookup();

            ProcessResidents(planetUrlLookup, personUrlLookup);
            ProcessHomeworlds(planetUrlLookup, personUrlLookup);
        }

        IDictionary<string, Planet> CreatePlanetUrlLookup()
        {
            Dictionary<string, Planet> planetUrlLookup = new Dictionary<string, Planet>();

            foreach (Planet p in Planets)
            {
                if (!planetUrlLookup.ContainsKey(p.Url))
                    planetUrlLookup.Add(p.Url, p);
                else 
                    throw new Exception(String.Format("We have multiple copies of the planet {0} from the API - something has gone wrong", p.Name));
            }

            return planetUrlLookup;
        }

        IDictionary<string, Person> CreatePersonUrlLookup()
        {
            Dictionary<string, Person> personUrlLookup = new Dictionary<string, Person>();
            foreach (Person p in People)
            {
                if (!personUrlLookup.ContainsKey(p.Url))
                    personUrlLookup.Add(p.Url, p);
                else
                    // Here I am assuming there are no duplicate names in Star Wars - not a good assumption in real life
                    throw new Exception(String.Format("We have multiple copies of the person {0} from the API - something has gone wrong", p.Name));
            }

            return personUrlLookup;
        }

        void ProcessResidents(IDictionary<string, Planet> planetUrlLookup, IDictionary<string, Person> personUrlLookup)
        {
            foreach (Planet p in planetUrlLookup.Values)
            {
                if (p.Name == null)
                    throw new Exception("Found planet with null name");

                if (!InhabitantLookup.ContainsKey(p.Name))
                    InhabitantLookup.Add(p.Name.ToLower(), new Population(p.Name));
                
                foreach (String personUrl in p.Residents)
                {
                    if (personUrlLookup.ContainsKey(personUrl))
                        InhabitantLookup[p.Name.ToLower()].AddResident(personUrlLookup[personUrl]);
                    else
                        throw new Exception(String.Format("Person {0} does not exist in person lookup table", personUrl));
                }
            }
        }

        void ProcessHomeworlds(IDictionary<string, Planet> planetLookup, IDictionary<string, Person> personLookup)
        {
            foreach (Person p in personLookup.Values)
            {
                if (p.Name == null)
                    throw new Exception("Found person with null name");

                string homeworldUrl = p.Homeworld;
                if (!planetLookup.ContainsKey(homeworldUrl))
                    throw new Exception(String.Format("World {0} does not exist in plant lookup table", homeworldUrl));
                
                Planet homeworld = planetLookup[homeworldUrl];
                if (homeworld.Name == null)
                    throw new Exception("Found planet with null name");

                if (!InhabitantLookup.ContainsKey(homeworld.Name.ToLower()))
                    InhabitantLookup.Add(homeworld.Name.ToLower(), new Population(homeworld.Name));
                InhabitantLookup[homeworld.Name.ToLower()].AddHomeworld(p);
            }
        }
    }
}
