using System.ComponentModel;

namespace Aries.Processors;

[Serializable]
public class Keeper : Processor
{
    public string XPath { set; get; }

    private bool Contains(XmlNodeList xmlNodeList, XmlNode node)
    {
        foreach (XmlNode childNode in xmlNodeList)
        {
            if (node == childNode)
            {
                return true;
            }
        }

        return false;
    }

    public override XDocument Process(XDocument doc)
    {

        var keepNodes = doc.XPathSelectElements(this.XPath);
        if (keepNodes == null)
        {
            return doc;
        }

        foreach (XElement keepNode in keepNodes)
        {
            var parent = keepNode.Parent;
            string tagName = keepNode.Name.LocalName;
            var deleteNodes = parent.Elements().ToList();
            foreach (var deleteNode in deleteNodes)
            {
                if(deleteNode.Name.LocalName != tagName)
                {
                    continue;
                }

                if (keepNodes.Contains(deleteNode))
                {
                    continue;
                }
                deleteNode.Remove();
            }
        }

        return doc;
    }
}
