using CodingChallenge.Lib;
using System;
using System.Collections.Generic;
using RestSharp;
using Moq;
using Xunit;

namespace CodingChallenge.Tests
{
    public class SWAPITests
    {
        SWAPI target;

        public SWAPITests()
        {
            
            target = new SWAPI();
        }

#region General Testing
        [Fact]
        public void UnsuccesfulRestCallThrowsException()
        {
            var MockRestClient = new Mock<IRestClient>();
            MockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(BuildFailedResponse(""));

            target.Client = MockRestClient.Object;
            Assert.Throws<Exception>(() => target.GetAllPlanets());
            Assert.Throws<Exception>(() => target.GetAllPeople());
        }
#endregion

#region Planet Testing
        [Fact]
        public void GetsSixPlanetsFromThreePagesTest()
        {
            var MockRestClient = new Mock<IRestClient>();
            MockRestClient.SetupSequence(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(BuildSuccessfulResponse(PLANET_PAGE_1))
                .Returns(BuildSuccessfulResponse(PLANET_PAGE_2))
                .Returns(BuildSuccessfulResponse(PLANET_PAGE_3));
            
            target.Client = MockRestClient.Object;
            IList<Planet> planets = target.GetAllPlanets();

            Assert.Equal(6, planets.Count);
        }

        [Fact]
        public void GetsNoPlanetsFromEmptyPageTest()
        {
            var MockRestClient = new Mock<IRestClient>();
            MockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(BuildSuccessfulResponse(EMPTY_PAGE));
            
            target.Client = MockRestClient.Object;
            IList<Planet> planets = target.GetAllPlanets();

            Assert.Equal(0, planets.Count);
        }
#endregion

#region  Person Testing
        [Fact]
        public void GetsSixPpeopleFromThreePagesTest()
        {
            var MockRestClient = new Mock<IRestClient>();
            MockRestClient.SetupSequence(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(BuildSuccessfulResponse(PEOPLE_PAGE_1))
                .Returns(BuildSuccessfulResponse(PEOPLE_PAGE_2))
                .Returns(BuildSuccessfulResponse(PEOPLE_PAGE_3));
            
            target.Client = MockRestClient.Object;
            IList<Person> people = target.GetAllPeople();

            Assert.Equal(6, people.Count);
        }

        [Fact]
        public void GetsNoPeopleFromEmptyPageTest()
        {
            var MockRestClient = new Mock<IRestClient>();
            MockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(BuildSuccessfulResponse(EMPTY_PAGE));
            
            target.Client = MockRestClient.Object;
            IList<Person> people = target.GetAllPeople();

            Assert.Equal(0, people.Count);
        }
#endregion

#region Setup Methods
        IRestResponse BuildSuccessfulResponse(string json)
        {
            Mock<IRestResponse> response = new Mock<IRestResponse>();
            response.Setup(x => x.IsSuccessful).Returns(true);
            response.Setup(x => x.Content).Returns(json);
            return response.Object;
        }

        IRestResponse BuildFailedResponse(string json)
        {
            Mock<IRestResponse> response = new Mock<IRestResponse>();
            response.Setup(x => x.IsSuccessful).Returns(false);
            response.Setup(x => x.Content).Returns(json);
            return response.Object;
        }
#endregion

#region Planet JSON
        const string PLANET_PAGE_1 = @"
        {
	        ""count"": 6,
	        ""next"": ""https://swapi.co/api/planets/?page=2"",
	        ""previous"": null,
	        ""results"": [
            {
                ""name"": ""Planet One"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/1/""
            },
            {""name"": ""Planet Two"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/2/""
            }]
        }";

        const string PLANET_PAGE_2 = @"
        {
	        ""count"": 6,
	        ""next"":  ""https://swapi.co/api/planets/?page=3"",
	        ""previous"": ""https://swapi.co/api/planets/?page=1"",
	        ""results"": [
            {
                ""name"": ""Planet Three"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/3/""
            },
            {""name"": ""Planet Four"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/4/""
            }]
        }";

        const string PLANET_PAGE_3 = @"
        {
	        ""count"": 6,
	        ""next"": null,
	        ""previous"": ""https://swapi.co/api/planets/?page=2"",
	        ""results"": [
            {
                ""name"": ""Planet Five"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/5/""
            },
            {""name"": ""Planet Six"",
                ""residents"": [
                    ""https://swapi.co/api/people/5/"",
                    ""https://swapi.co/api/people/68/"",
                    ""https://swapi.co/api/people/81/""
                ],
                ""films"": [
                    ""https://swapi.co/api/films/6/"",
                    ""https://swapi.co/api/films/1/""
                ],
                ""created"": ""2014-12-10T11:35:48.479000Z"",
                ""edited"": ""2014-12-20T20:58:18.420000Z"",
                ""url"": ""https://swapi.co/api/planets/6/""
            }]
        }";
#endregion

#region People JSON
        const string PEOPLE_PAGE_1 = @"
        {
            ""count"": 87,
            ""next"": ""https://swapi.co/api/people/?page=2"",
            ""previous"": null,
            ""results"": [
                {
                    ""name"": ""Person One"",
                    ""homeworld"": ""https://swapi.co/api/planets/1/"",
                    ""created"": ""2014-12-09T13:50:51.644000Z"",
                    ""edited"": ""2014-12-20T21:17:56.891000Z"",
                    ""url"": ""https://swapi.co/api/people/1/""
                },
                {
                    ""name"": ""Person Two"",
                    ""homeworld"": ""https://swapi.co/api/planets/1/"",
                    ""created"": ""2014-12-10T15:10:51.357000Z"",
                    ""edited"": ""2014-12-20T21:17:50.309000Z"",
                    ""url"": ""https://swapi.co/api/people/2/""
                }
            ]
        }";

        const string PEOPLE_PAGE_2 = @"
        {
            ""count"": 87,
            ""next"": ""https://swapi.co/api/people/?page=2"",
            ""previous"": ""https://swapi.co/api/people/?page=1"",
            ""results"": [
                {
                    ""name"": ""Person Three"",
                    ""homeworld"": ""https://swapi.co/api/planets/3/"",
                    ""created"": ""2014-12-09T13:50:51.644000Z"",
                    ""edited"": ""2014-12-20T21:17:56.891000Z"",
                    ""url"": ""https://swapi.co/api/people/1/""
                },
                {
                    ""name"": ""Person 4"",
                    ""homeworld"": ""https://swapi.co/api/planets/5/"",
                    ""created"": ""2014-12-10T15:10:51.357000Z"",
                    ""edited"": ""2014-12-20T21:17:50.309000Z"",
                    ""url"": ""https://swapi.co/api/people/2/""
                }
            ]
        }";

        const string PEOPLE_PAGE_3 = @"
        {
            ""count"": 6,
            ""next"": null,
            ""previous"": ""https://swapi.co/api/people/?page=2"",
            ""results"": [
                {
                    ""name"": ""Person Five"",
                    ""homeworld"": ""https://swapi.co/api/planets/5/"",
                    ""created"": ""2014-12-09T13:50:51.644000Z"",
                    ""edited"": ""2014-12-20T21:17:56.891000Z"",
                    ""url"": ""https://swapi.co/api/people/1/""
                },
                {
                    ""name"": ""Person Six"",
                    ""homeworld"": ""https://swapi.co/api/planets/6/"",
                    ""created"": ""2014-12-10T15:10:51.357000Z"",
                    ""edited"": ""2014-12-20T21:17:50.309000Z"",
                    ""url"": ""https://swapi.co/api/people/2/""
                }
            ]
        }";
#endregion

#region General Pages
        const string EMPTY_PAGE = @"
        {
	        ""count"": 0,
	        ""next"": null,
	        ""previous"": null,
	        ""results"": []
        }";

#endregion
    }
}