namespace SerializationLibrary;

using System.Xml.Serialization;
using PeopleLibrary;
using static System.Environment;
using static System.IO.Path;
using FastJson = System.Text.Json.JsonSerializer;

public class Serializer
{
    public string jsonFileName = "";
    public string jsonPath = "";
    public string xmlFileName = "";
    public string xmlPath = "";

    public List<Person> peopleData;

    public Serializer(List<Person> pd, string jf = "data.json",
        string xf = "data.xml")
    {
        peopleData = pd;
        jsonFileName = jf;
        xmlFileName = xf;
        string currentDirectory = System.IO.Directory.
            GetParent(CurrentDirectory)?.ToString() ?? "";
        jsonPath = Combine(currentDirectory, jsonFileName);
        xmlPath = Combine(currentDirectory, xmlFileName);
    }

    public void Serialize()
    {
        // Serialize XML
        XmlSerializer xs = new(type: peopleData.GetType());
        using (FileStream stream = File.Create(xmlPath)) {
            xs.Serialize(stream, peopleData);
        }

        // Serialize JSON
        using (StreamWriter jsonStream = File.CreateText(jsonPath)) {
            Newtonsoft.Json.JsonSerializer jss = new();
            jss.Serialize(jsonStream, peopleData);
        }
    }

    /// <summary>
    /// Deserializes files into peopleData field.
    /// </summary>
    /// <Usage>List<Person> x = await serializer.Deserialize() </Usage>
    public async Task<List<Person>> Deserialize()
    {
        List<Person>? jsonPeople = new();
        List<Person>? xmlPeople = new();

        // Deserialize JSON
        using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open)) {
            jsonPeople = await FastJson.DeserializeAsync(utf8Json: jsonLoad,
                returnType: typeof(List<Person>)) as List<Person>;
        }

        // Deserialize XML
        var xs = new XmlSerializer(typeof(List<Person>));
        using (FileStream xmlLoad = File.Open(xmlPath, FileMode.Open)) {
            xmlPeople = (List<Person>)xs.Deserialize(xmlLoad);
        }
        // Lazy check if equal data is read. Further checking can be done in unit tests
        if (jsonPeople.Count != xmlPeople.Count)
            throw new Exception("Read different data from json and xml files!");

        // Can either be jsonPeople or xmlPeople
        peopleData = jsonPeople;
        return peopleData;
    }
}
