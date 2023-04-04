namespace src;

using PeopleLibrary;
using SerializationLibrary;
using static System.Console;

class Program
{
    public static List<Person> people;
    public static Serializer sc;
    public static string user;
    public static string password;

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

        while (true) {
            readCredentials();
            WriteLine($"User is {user}");
            WriteLine($"Password is {password.ToString()}");
        }
    }

    private static void readCredentials()
    {
        do {
            Write("Please enter your username: ");
            user = ReadLine();
        } while (user is null);

        do {
            Write("Please enter your password: ");
            password = getPassword();
        } while (password.Length == 0);

        WriteLine();
    }

    private static string getPassword()
    {
        System.Text.StringBuilder sb = new();
        ConsoleKeyInfo key;

        do {
            key = Console.ReadKey(true);

            // Ignore any key out of range.
            if (!char.IsControl(key.KeyChar)) {
                // Append the character to the password.
                sb.Append(key.KeyChar);
                Console.Write("*");
            }
            // Exit if Enter key is pressed.
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        return sb.ToString();
    }
}
