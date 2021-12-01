

namespace Aries.Processors;

[Serializable]
public class Filter : Processor
{
    public string XPath { set; get; }

    public override XDocument Process(XDocument doc)
    {

        var nodes = doc.XPath2SelectElements(XPath);
        if (nodes == null)
        {
            return doc;
        }

        foreach (var node in nodes)
        {
            node.Remove();
        }

        return doc;
    }
}
