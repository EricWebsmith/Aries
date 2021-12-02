namespace Aries.Processors;

public enum Scope
{
    All,
    Page
}

[Serializable]
public class AnchorFilter : Processor
{
    public Scope Scope { get; set; } = Scope.Page;
    public string AnchorXPath { get; set; }
    public string XPath { set; get; }

    private XElement ProcessOne(XElement element)
    {
        var anchors = element.XPath2SelectElements(AnchorXPath).ToList();
        foreach (var anchor in anchors)
        {
            string realXPath = XPath;
            foreach (var attribute in anchor.Attributes())
            {
                string key = attribute.Name.LocalName;
                string value = attribute.Value;
                realXPath = realXPath.Replace($"{{{key}}}", value);
            }

            var nodes = element.XPath2SelectElements(realXPath).ToList();
            if (nodes == null)
            {
                return element;
            }

            foreach (var node in nodes)
            {
                node.Remove();
            }
        }

        return element;
    }

    public override XDocument Process(XDocument doc)
    {
        XElement root = doc.Root;

        switch (Scope)
        {
            case Scope.All:
                ProcessOne(doc.Root);
                break;
            case Scope.Page:
                foreach (var page in doc.Root.Elements("page"))
                {
                    ProcessOne(page);
                }
                break;
            default:
                break;
        }
        
        return doc;
    }
}
