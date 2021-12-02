using Newtonsoft.Json;

namespace Aries.Test;

[TestClass]
public class CustomSerializationTests
{
    [TestMethod]
    public void SerializeTest()
    {
        Filter filter = new Filter();
        filter.XPath = "1";

        AnchorFilter anchorFilter = new AnchorFilter();
        anchorFilter.XPath = "2";

        ProcessorContainer processorContainer = new ProcessorContainer();
        processorContainer.Add(filter);
        processorContainer.Add(anchorFilter);

        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.Formatting = Formatting.Indented;
        jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
        jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        var json = JsonConvert.SerializeObject(processorContainer, jsonSerializerSettings);
        Console.WriteLine(json);

        var newObj = JsonConvert.DeserializeObject<ProcessorContainer> (json, jsonSerializerSettings);
        Console.WriteLine(newObj);
        var newJson = JsonConvert.SerializeObject(newObj, jsonSerializerSettings);
        Console.WriteLine(json);
    }
}

