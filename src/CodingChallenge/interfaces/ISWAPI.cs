using System.Collections.Generic;
using CodingChallenge.Lib;

namespace CodingChallenge.Interfaces
{
    public interface ISWAPI
    {
        IList<Person> GetAllPeople();
        IList<Planet> GetAllPlanets();
    }
}