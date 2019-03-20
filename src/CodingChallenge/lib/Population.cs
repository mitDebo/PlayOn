using System;
using System.Collections.Generic;

namespace CodingChallenge.Lib
{
    public class Population
    {
        public string PlanetName { get; set; }
        public IList<Person> Residents { get; }
        public IList<Person> Homeworld { get; }
        public ISet<Person> All { get; }

        public Population(string planetName)
        {
            PlanetName = planetName;
            Residents = new List<Person>();
            Homeworld = new List<Person>();
            All = new HashSet<Person>();
        }
        
        /// <summary>
        /// Adds a resident to a population of a planet
        /// </summary>
        public void AddResident(Person person)
        {
            Residents.Add(person);
            All.Add(person);
        }

        /// <summary>
        /// Adds a person that claims this planet as their homeworld
        /// </summary>
        public void AddHomeworld(Person person)
        {
            Homeworld.Add(person);
            All.Add(person);
        }

        /// <summary>
        /// Print all the residents of the world
        /// </summary>
        public void PrintResidents()
        {
            Console.WriteLine("Residents of {0}:", PlanetName);
            foreach (Person p in Residents)
                Console.WriteLine("\t{0}", p.Name);
        }

        /// <summary>
        /// Print everyone that calls this world their homeworld
        /// </summary>
        public void PrintHomeworld()
        {
            Console.WriteLine("People that call {0} thier homeworld:", PlanetName);
            foreach (Person p in Homeworld)
                Console.WriteLine("\t{0}", p.Name);
        }

        /// <summary>
        /// Print everyone that is either a resident or calls this world their homeworld
        /// </summary>
        public void PrintAll()
        {
            Console.WriteLine("Everyone that has lived on {0}:", PlanetName);
            foreach (Person p in All)
                Console.WriteLine("\t{0}", p.Name);
        }
    }
}