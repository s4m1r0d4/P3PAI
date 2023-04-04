namespace src;
using PeopleLibrary;
using SerializationLibrary;
using static System.Console;
class Program
{
    public static List<Person> people;
    public static Serializer sc;

    Program()
    {
        sc = new(people);
    }

    public static async Task Main(string[] args)
    {
        try {
            people = await sc.Deserialize();
        } catch {
            WriteLine($"Couldn't find local database, project will generate a new one");
            people = new();
        }

        if (people.Count != 1)
            WriteLine($"{people.Count} people have been loaded");
        else
            WriteLine($"{people.Count} person has been loaded");
    }
}
