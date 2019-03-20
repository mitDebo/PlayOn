using System.Collections.Generic;
using CodingChallenge.Lib;

namespace CodingChallenge.Interfaces
{
    public interface ISWAPI
    {
        List<Person> GetAllPeople();
        List<Planet> GetAllPlanets();
    }
}