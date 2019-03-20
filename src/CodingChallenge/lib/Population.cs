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

        public void AddResident(Person person)
        {
            Residents.Add(person);
            All.Add(person);
        }

        public void AddHomeworld(Person person)
        {
            Homeworld.Add(person);
            All.Add(person);
        }

        public void PrintResidents()
        {
            Console.WriteLine(String.Format("Residents of {0}:", PlanetName));
            foreach (Person p in Residents)
                Console.WriteLine(String.Format("\t{0}", p.Name));
        }

        public void PrintHomeworld()
        {
            Console.WriteLine(String.Format("People that call {0} thier homeworld:", PlanetName));
            foreach (Person p in Homeworld)
                Console.WriteLine(String.Format("\t{0}", p.Name));
        }

        public void PrintAll()
        {
            Console.WriteLine(String.Format("Everyone that has lived on {0}:", PlanetName));
            foreach (Person p in All)
                Console.WriteLine(String.Format("\t{0}", p.Name));
        }
    }
}