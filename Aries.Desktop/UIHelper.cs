using System.Xml.Linq;

namespace Aries.Desktop;

public static class UIHelper
{
    public static List<Type> ProcessorTypes { get; private set; }

    static UIHelper()
    {
        ProcessorTypes = new List<Type>();
        Type processorType = typeof(Processor);
        Type[] types = processorType.Assembly.GetTypes();
        foreach (Type type in types)
        {
            if (type.IsSubclassOf(processorType))
            {
                ProcessorTypes.Add(type);
            }
        }
    }

    public static void Run(XDocument doc, ProcessorContainer processors)
    {
        doc = new XDocument(doc);
        if (!Directory.Exists("data")) { Directory.CreateDirectory("data"); }

        //List<XDocument> results = new List<XDocument>();
        //results.Add(new XDocument(doc));
        doc.Save("data/0.xml");
        string html = Xml2Html.ToHtml(doc, 0);
        File.WriteAllText($"data/0.html", html);
        for (int i = 0; i < processors.Count; i++)
        {
            var p = processors[i];
            doc = p.Process(doc);
            // results.Add(new XDocument(doc));
            doc.Save($"data/{i + 1}.xml");
            html = Xml2Html.ToHtml(doc, i+1);
            File.WriteAllText($"data/{i + 1}.html", html);
        }

        //return results;
    }
}

