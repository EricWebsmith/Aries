using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Aries.Test;

[TestClass]
public class SerializationTests
{

    [TestMethod]
    public void JsonSerializeTest()
    {
        ProcessorContainer container = new ProcessorContainer();

        Filter filter = new Filter();
        filter.XPath =  "//page[@number='1']";
        container.Add(filter);

        Keeper keeper = new Keeper();
        keeper.XPath = "//page/text[number(@top)<200 and text()='Statement of Assets and Liabilities']/ancestor::page";
        container.Add(keeper);

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        string jsonFile = @"D:\projects\Aries\sample.json";
        if (File.Exists(jsonFile)) { File.Delete(jsonFile); }
        StreamWriter streamWriter = new StreamWriter(jsonFile);
        JsonWriter jsonWriter = new JsonTextWriter(streamWriter);
        serializer.Serialize(jsonWriter, container);
        jsonWriter.Close();
        streamWriter.Close();

        Console.WriteLine("Hello, World!");
    }

    [TestMethod]
    public void XmlSerializeTest()
    {
        ProcessorContainer container = new ProcessorContainer();

        Filter filter = new Filter();
        filter.XPath = "//page[@number='1']";
        container.Add(filter);

        Keeper keeper = new Keeper();
        keeper.XPath = "//page/text[number(@top)<200 and text()='Statement of Assets and Liabilities']/ancestor::page";
        container.Add(keeper);

        XmlSerializer serializer = new XmlSerializer(typeof(Keeper));
        string jsonFile = @"D:\projects\Aries\sample.config.xml";
        if (File.Exists(jsonFile)) { File.Delete(jsonFile); }
        StreamWriter streamWriter = new StreamWriter(jsonFile);
        serializer.Serialize(streamWriter, keeper);
        streamWriter.Close();

        Console.WriteLine("Hello, World!");
    }

    [TestMethod]
    public void BinarySerializeTest()
    {
        ProcessorContainer container = new ProcessorContainer();

        Filter filter = new Filter();
        filter.XPath = "//page[@number='1']";
        container.Add(filter);

        Keeper keeper = new Keeper();
        keeper.XPath = "//page/text[number(@top)<200 and text()='Statement of Assets and Liabilities']/ancestor::page";
        container.Add(keeper);

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(@"D:\projects\Aries\MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, container);
        stream.Close();
    }

    [TestMethod]
    public void BinaryDeserializeTest()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(@"D:\projects\Aries\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.None);
        object obj = formatter.Deserialize(stream);
        stream.Close();

        ProcessorContainer container = (ProcessorContainer)obj;
    }
}
