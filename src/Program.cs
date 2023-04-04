namespace src;
using PeopleLibrary;
using SerializationLibrary;
class Program
{
    static void Main(string[] args)
    {
        List<Person> lp = new();
        Console.WriteLine("Hello, World!");
        Serializer sc = new(lp);
        sc.Serialize();
    }
}
