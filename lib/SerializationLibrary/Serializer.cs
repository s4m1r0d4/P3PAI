namespace SerializationLibrary;

using System.Xml.Serialization;
using PeopleLibrary;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using FastJson = System.Text.Json.JsonSerializer;

public class Serializer
{
    static readonly string jsonFileName = "data.json";
    static readonly string xmlFileName = "data.xml";
    List<Person>? peopleData;

    public Serializer(List<Person>? pd)
    {
        peopleData = pd;
    }

    public void Serialize()
    {
        string currentDirectory = System.IO.Directory.GetParent(CurrentDirectory)?.ToString() ?? "";
        string jsonPath = Combine(currentDirectory, jsonFileName);
        string xmlPath = Combine(currentDirectory, xmlFileName);

        /* WriteLine($"Json path is: '{jsonPath}'");
        WriteLine($"XML path is: '{xmlPath}'"); */

    }
}
