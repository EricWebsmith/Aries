namespace Aries.Processors;

[Serializable]
public class FontExtractor: Processor
{
    public override XDocument Process(XDocument doc)
    {
        var fontspecs = doc.XPathSelectElements("/pdf2xml/page/fontspec").ToList();
        //doc.Root.Add();
        XElement fonts = new XElement("fonts");
        foreach (var font in fontspecs)
        {
            font.Remove();
            fonts.Add(font);
        }

        doc.Root?.AddFirst(fonts);

        return doc;
    }
}

