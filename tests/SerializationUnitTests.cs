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
    public async Task DeserializeWithChildren()
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
        Serializer sc = new(people, "JsonTest2.json", "XmlTest2.xml");
        // Load files with info
        sc.Serialize();
        await sc.Deserialize();
        people = sc.peopleData;

        WriteLine(File.ReadAllText(sc.jsonPath));
        WriteLine(File.ReadAllText(sc.xmlPath));

        Assert.True(people.Count > 0, "Failed reading info from files");
    }

    [Fact]
    public async Task ReadDifferentData()
    {
        List<Person> people1 = new()
        {
            new()
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
            }
        };

        Serializer sc1 = new(people1, "JsonTest3.json", "XmlTest3.xml");
        sc1.Serialize();

        List<Person> people2 = new()
        {
            new()
            {
                Salary = 1000M,
                FirstName = "Derfla",
                LastName = "Ayana",
                DateOfBirth = new(year: 1002, month: 3, day: 13),
                Children = new()
                {
                    new()
                    {
                        Salary = 5M,
                        FirstName = "Ching",
                        LastName = "Chong",
                        DateOfBirth = new(year: 2020, month: 03, day: 15),
                        Children = null,
                        BankAccount = 123,
                        BankPassword = "jhon wick 1"

                    }
                },
                BankAccount = 87,
                BankPassword = "el antilolero 83"
            }
        };
        Serializer sc2 = new(people2, "JsonTest4.json", "XmlTest4.xml");
        sc2.Serialize();

        await sc1.Deserialize();
        await sc2.Deserialize();

        List<Person> readOne = sc1.peopleData;
        List<Person> readTwo = sc2.peopleData;

        Assert.True(readOne != readTwo, "Bro u messed up fr fr");
    }

    [Fact]
    public async Task ReadSameAsWritten()
    {
        List<Person> written = new()
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

        Serializer sc = new(written, "JsonTest5.json", "XmlTest5.xml");
        sc.Serialize();
        List<Person> read = await sc.Deserialize();

        Assert.True(written.Count == read.Count, "bro this async stuff be crazy fr fr");
    }

    [Fact]
    public async void OpenNonExistingFiles()
    {
        List<Person> people = new();
        Serializer sc = new(people, "JsonTest6.json", "XmlTest6.xml");
        bool exceptionThrown = false;

        try {
            await sc.Deserialize();
        } catch {
            exceptionThrown = true;
        }

        Assert.True(exceptionThrown, "Expected exception deserializing non-existing files");
    }
}