namespace Aries.Processors;

public class NumberDetector: Processor
{
    public override XDocument Process(XDocument doc)
    {
        string xpath = @"/page/text[matches(text(), '\(?\d{1,3}(,\d{3})*\)?')]";
        List<XElement> texts = doc.Root.XPath2SelectElements(xpath).ToList();
        foreach(XElement text in texts)
        {
            text.Add(new XAttribute("number", "true"));
        }

        return doc;
    }

}
