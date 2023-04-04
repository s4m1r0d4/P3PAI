namespace tests;
using PeopleLibrary;
using SerializationLibrary;
using static System.Console;

public class SerializationUnitTests
{
    [Fact]
    public void SerializeWithChildren()
    {
        List<Person> people = new()
        {
            new ()
            {
                Salary = 1000M,
                FirstName = "Alfred",
                LastName = "Anaya",
                DateOfBirth = new(year: 2001, month: 2, day: 14),
                Children = new()
                {
                    new()
                    {
                        Salary = 5000M,
                        FirstName = "Chong",
                        LastName = "Chang",
                        DateOfBirth = new(year: 2020, month: 03, day: 15),
                        Children = null,
                        BankAccount = 1543,
                        BankPassword = "jhon wick 5"

                    }
                },
                BankAccount = 1234,
                BankPassword = "el lolero 83"
            },
            new ()
            {
                Salary = 10000M,
                FirstName = "Minerva",
                LastName = "Rivera",
                DateOfBirth = new(year: 2000, month: 8, day: 22),
                Children = new()
                {
                    new()
                    {
                        Salary = 5000M,
                        FirstName = "Quien",
                        LastName = "Sabe",
                        DateOfBirth = new(year: 2001, month: 05, day: 17),
                        Children = null,
                        BankAccount = 12123434,
                        BankPassword = "me choca el 51C"
                    }
                },
                BankAccount = 89834,
                BankPassword = "puro corrido tumbado"
            }
        };

        Serializer sc = new(people, "JsonTest.json", "XmlTest.xml");
        sc.Serialize();
    }

    [Fact]
    public void DeserializeWithChildren()
    {
        List<Person> people = new();
        Serializer sc = new(people, "JsonTest.json", "XmlTest.xml");
        // Load files with info
        SerializeWithChildren();
        sc.Deserialize();
        people = sc.peopleData;

        WriteLine(File.ReadAllText(sc.jsonPath));
        WriteLine(File.ReadAllText(sc.xmlPath));

        Assert.True(people.Count > 0, "Failed reading info from files");
    }
}