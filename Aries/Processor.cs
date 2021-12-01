using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aries;

[Serializable]
public class Processor
{
    /// <summary>
    /// Name, if not none, will be displayed
    /// Otherwise, the processor class name will be used.
    /// </summary>
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual XDocument Process(XDocument doc) { return doc; }
}

[Serializable]
public class ProcessorContainer : List<Processor> { }

public static class ProcessorSerializer
{
    public static void Serialize(object obj, string path)
    {
        IFormatter formatter = new BinaryFormatter();
        if (File.Exists(path)) { File.Delete(path); }
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static ProcessorContainer Deserialize(string path)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
        object obj = formatter.Deserialize(stream);
        stream.Close();

        ProcessorContainer container = (ProcessorContainer)obj;
        return container;
    }
}