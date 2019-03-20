using CodingChallenge.Lib;
using Xunit;

namespace CodingChallenge.Tests
{
    public class PopulationTests
    {
        Person person1;
        Person person2;
        Person person3;
        Person person4;
        Person person5;
        Population target;

        public PopulationTests()
        {
            person1 = new Person() { Name = "Person 1" };
            person2 = new Person() { Name = "Person 2" };
            person3 = new Person() { Name = "Person 3" };
            person4 = new Person() { Name = "Person 4" };
            person5 = new Person() { Name = "Person 5" };

            target = new Population("Test pop");
        }

        [Fact]
        public void AddResidentsAndAddHomeworldAlsoAddsToAllTest()
        {
            target.AddResident(person1);
            target.AddResident(person2);
            target.AddResident(person3);

            Assert.Equal(3, target.Residents.Count);
            Assert.Equal(3, target.All.Count);

            target.AddHomeworld(person4);
            target.AddHomeworld(person5);

            Assert.Equal(2, target.Homeworld.Count);
            Assert.Equal(5, target.All.Count);

            Assert.True(target.Residents.Contains(person2));
            Assert.True(target.Homeworld.Contains(person5));
            
            Assert.False(target.Homeworld.Contains(person3));
            Assert.False(target.Residents.Contains(person5));
            
            Assert.True(target.All.Contains(person2));
            Assert.True(target.All.Contains(person5));
        }

        [Fact]
        public void AddingSamePersonToBothResidentAndHomeworldTest()
        {
            target.AddResident(person1);
            target.AddResident(person2);
            
            target.AddHomeworld(person2);
            target.AddHomeworld(person3);

            Assert.Equal(3, target.All.Count);
        }
    }
}