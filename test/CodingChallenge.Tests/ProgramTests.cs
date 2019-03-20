using CodingChallenge.Lib;
using CodingChallenge.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CodingChallenge.Tests
{
    public class ProgramTests
    {
        public IList<Person> AllPeople = new List<Person>() {
            new Person() { Name = "Person One", Homeworld = "https://swapi.co/api/planets/1/", Url = "https://swapi.co/api/people/1/"},
            new Person() { Name = "Person Two", Homeworld = "https://swapi.co/api/planets/2/", Url = "https://swapi.co/api/people/2/"},
            new Person() { Name = "Person Three", Homeworld = "https://swapi.co/api/planets/1/", Url = "https://swapi.co/api/people/3/"},
            new Person() { Name = "Person Four", Homeworld = "https://swapi.co/api/planets/3/", Url = "https://swapi.co/api/people/4/"},
            new Person() { Name = "Person Five", Homeworld = "https://swapi.co/api/planets/3/", Url = "https://swapi.co/api/people/5/"}
        };

        public IList<Planet> AllPlanets = new List<Planet>() {
            new Planet() { Name = "Planet One", 
                Residents = new List<string>() 
                {
                    "https://swapi.co/api/people/1/",
                    "https://swapi.co/api/people/3/"
                },
                Url = "https://swapi.co/api/planets/1/"
            },
            new Planet() { Name = "Planet Two", 
                Residents = new List<string>()
                {
                    "https://swapi.co/api/people/2/"
                },
                Url = "https://swapi.co/api/planets/2/"
            },
            new Planet() { Name = "Planet Three", 
                Residents = new List<string>()
                {
                    "https://swapi.co/api/people/4/",
                    "https://swapi.co/api/people/5/"
                },
                Url = "https://swapi.co/api/planets/3/"
            }
        };

        [Fact]
        public void AllPeopleAndAllPlanetsTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();
            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            target.Init();

            Assert.Equal(AllPeople.Count, target.People.Count);
            Assert.Equal(AllPlanets.Count, target.Planets.Count);
        }

        [Fact]
        public void InhabitantLookupTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();
            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            target.Init();

            Assert.Equal(2, target.InhabitantLookup["planet one"].Residents.Count);
            Assert.Equal(2, target.InhabitantLookup["planet one"].Homeworld.Count);
            Assert.Equal(2, target.InhabitantLookup["planet one"].All.Count);

            Assert.Equal(1, target.InhabitantLookup["planet two"].Residents.Count);
            Assert.Equal(1, target.InhabitantLookup["planet two"].Homeworld.Count);
            Assert.Equal(1, target.InhabitantLookup["planet two"].All.Count);

            Assert.Equal(2, target.InhabitantLookup["planet three"].Residents.Count);
            Assert.Equal(2, target.InhabitantLookup["planet three"].Homeworld.Count);
            Assert.Equal(2, target.InhabitantLookup["planet three"].All.Count);
        }

        [Fact]
        public void AddAdditionalPersonToHomeworldTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            Person person6 = new Person() {
                Name = "Person Six",
                Homeworld = "https://swapi.co/api/planets/1/",
                Url = "https://swapi.co/api/people/6/"
            };
            AllPeople.Add(person6);

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            target.Init();

            Assert.Equal(3, target.InhabitantLookup["planet one"].Homeworld.Count);
            Assert.Equal(2, target.InhabitantLookup["planet one"].Residents.Count);
            Assert.True(target.InhabitantLookup["planet one"].Homeworld.Contains(person6));
            Assert.False(target.InhabitantLookup["planet one"].Residents.Contains(person6));
            Assert.False(target.InhabitantLookup["planet two"].All.Contains(person6));
        }

        [Fact]
        public void AddAdditionalPersonToResidentsTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            Person person6 = new Person() {
                Name = "Person Six",
                Homeworld = "https://swapi.co/api/planets/1/",
                Url = "https://swapi.co/api/people/6/"
            };
            AllPeople.Add(person6);

            AllPlanets[1].Residents.Add("https://swapi.co/api/people/6/");

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            target.Init();
            Assert.Equal(1, target.InhabitantLookup["planet two"].Homeworld.Count);
            Assert.Equal(2, target.InhabitantLookup["planet two"].Residents.Count);
            Assert.True(target.InhabitantLookup["planet two"].Residents.Contains(person6));
            Assert.False(target.InhabitantLookup["planet two"].Homeworld.Contains(person6));
            Assert.False(target.InhabitantLookup["planet three"].All.Contains(person6));
        }

        [Fact]
        public void PlanetNameNullCheckTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            AllPlanets.Add(new Planet() { Name = null, Residents = new List<string>(), Url = "https://swapi.co/api/planets/4/"});

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());
        }

        [Fact]
        public void PersonNameNullCheckTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            AllPeople.Add(new Person() { Name = null, Homeworld = "https://swapi.co/api/planets/4/", Url = "https://swapi.co/api/people/6/"});

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());
        }

        [Fact]
        public void CantFindPersonInPeopleTableTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            AllPlanets[0].Residents.Add("https://swapi.co/api/people/notreal/");

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());    
        }

        [Fact]
        public void CantFindPlanetInPlanetTableTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            Person person6 = new Person() {
                Name = "Person Six",
                Homeworld = "https://swapi.co/api/planets/notreal/",
                Url = "https://swapi.co/api/people/6/"
            };
            AllPeople.Add(person6);

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());    
        }

        [Fact]
        public void TryToInsertDuplicatePersonIntoPeopleTableTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            Person person6 = new Person() {
                Name = "Person One",
                Homeworld = "https://swapi.co/api/planets/notreal/",
                Url = "https://swapi.co/api/people/1/"
            };
            AllPeople.Add(person6);

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());    
        }

        [Fact]
        public void TryToInsertDuplicatePlanetIntoPlanetTableTest()
        {
            var MockSWAPI = new Mock<ISWAPI>();

            Planet planet4 = new Planet() {
                Name = "Planet One",
                Residents = new List<string>() 
                {
                    "https://swapi.co/api/people/1/",
                    "https://swapi.co/api/people/3/"
                },
                Url = "https://swapi.co/api/planets/1/"
            };
            AllPlanets.Add(planet4);

            MockSWAPI.Setup(x => x.GetAllPeople()).Returns(AllPeople);
            MockSWAPI.Setup(x => x.GetAllPlanets()).Returns(AllPlanets);
            Program target = new Program();
            target.SWAPI = MockSWAPI.Object;

            Assert.Throws<Exception>(() => target.Init());    
        }
    }
}