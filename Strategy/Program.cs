using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public int FavouriteNumber { get; set; }
    }

    class Program
    {
        // wrong: comparision filter is built-in
        static Person GetMaxPersonByAge(List<Person> people)
        {
            var maxPerson = people[0];

            for (int i = 0; i < people.Count; i++)
            {
                if (maxPerson.Age < people[i].Age)
                    maxPerson = people[i];
            }

            return maxPerson;
        }

        // comparision filter is injected
        // problem: still, only a small part of the logic can be injected
        static Person GetMaxPerson(List<Person> people, Func<Person, int> getPersonData)
        {
            var maxPerson = people[0];

            for (int i = 0; i < people.Count; i++)
            {
                if (getPersonData(maxPerson) < getPersonData(people[i]))
                    maxPerson = people[i];
            }

            return maxPerson;
        }

        // to support comparing other than int f.e. string
        // problem: still, only a small part of the logic can be injected
        static Person GetMaxPerson(List<Person> people, Func<Person, Person, int> getPersonData)
        {
            var maxPerson = people[0];

            for (int i = 0; i < people.Count; i++)
            {
                if (getPersonData(maxPerson, people[i]) < 0)
                {
                    maxPerson = people[i];
                }
            }

            return maxPerson;
        }

        // correct strategy pattern
        // if multiple filters needs to be injected -> multiple methods on the interface
        public interface IExtractorStrategy
        {
            int ExtractPersonData(Person p);
        }

        public class AgeExtractor : IExtractorStrategy
        {
            public int ExtractPersonData(Person p)
            {
                return p.Age;
            }
        }

        static Person GetMaxPerson(List<Person> people, IExtractorStrategy extractor)
        {
            var maxPerson = people[0];

            for (int i = 0; i < people.Count; i++)
            {
                if (extractor.ExtractPersonData(maxPerson) < extractor.ExtractPersonData(people[i]))
                    maxPerson = people[i];
            }

            return maxPerson;
        }

        static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person() {Age = 19, Name = "Andras", FavouriteNumber = 19},
                new Person() {Age = 30, Name = "Bela", FavouriteNumber = 23},
                new Person() {Age = 10, Name = "Cecil", FavouriteNumber = 56},
            };

            var maxAgePerson = GetMaxPerson(people, p => p.Age);
            var maxAgePersonv2 = GetMaxPerson(people, (p1, p2) => p1.Age.CompareTo(p2.Age));
            var maxFavNumPerson = GetMaxPerson(people, p => p.FavouriteNumber);
            var maxNamePerson = GetMaxPerson(people, (p1, p2) => string.Compare(p1.Name, p2.Name));


            var maxAgePersonv3 = GetMaxPerson(people, new AgeExtractor());
        }
    }
}
