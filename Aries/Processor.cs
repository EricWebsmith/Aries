using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Aries;

[Serializable]
public class Processor
{
    public string Description { get; set; } = string.Empty;
    public virtual XDocument Process(XDocument doc) { return doc; }
}

[Serializable]
public class ProcessorContainer : List<Processor> { }

public static class ProcessorSerializer
{
    public static void Serialize(object obj, string path)
    {
        IFormatter formatter = new ProcessorFormatter();
        if (File.Exists(path)) { File.Delete(path); }
        using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            formatter.Serialize(stream, obj);
            stream.Close();
        }
    }

    public static ProcessorContainer Deserialize(string path)
    {
        IFormatter formatter = new ProcessorFormatter();
        object obj = null;
        using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            obj = formatter.Deserialize(stream);
            stream.Close();
        }

        ProcessorContainer container = (ProcessorContainer)obj;
        return container;
    }


}

public class ProcessorFormatter : IFormatter
{
    public SerializationBinder? Binder { get; set; }
    public StreamingContext Context { get; set; }
    public ISurrogateSelector? SurrogateSelector { get; set; }

    public object Deserialize(Stream serializationStream)
    {
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
        jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        string json = string.Empty;

        using (StreamReader reader = new StreamReader(serializationStream))
        {
            json = reader.ReadToEnd();
        }



        return JsonConvert.DeserializeObject(json, jsonSerializerSettings);
    }

    public void Serialize(Stream serializationStream, object graph)
    {
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
        jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        var json = JsonConvert.SerializeObject(graph, jsonSerializerSettings);

        using (StreamWriter sw = new StreamWriter(serializationStream))
        {
            sw.Write(json);
            sw.Flush();
            sw.Close();

        }
    }
}