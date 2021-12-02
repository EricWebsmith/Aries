using System.Text.RegularExpressions;

namespace Aries.Processors;

[Serializable]
public class Replacer: Processor
{
    public string XPath { get; set; } = "//text";
    public string Pattern { get; set; } = string.Empty;
    public string Replacement { get; set; } = string.Empty;

    public override XDocument Process(XDocument doc)
    {
        Regex r = new Regex(Pattern, RegexOptions.Compiled);
        var elements = doc.Root.XPath2SelectElements(XPath);
        foreach (var element in elements)
        {
            element.Value = r.Replace(element.Value,Replacement);
        }

        return base.Process(doc);
    }
}

