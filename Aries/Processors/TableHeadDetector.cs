namespace Aries.Processors;

public class Box
{
    public int Left { get; set; } = int.MaxValue;
    public int Right { get; set; } = int.MinValue;
    public int Top { get; set; } = int.MaxValue;
    public int Bottom { get; set; } = int.MinValue;
}

[Serializable]
public class TableHeadDetector: Processor
{
    public override XDocument Process(XDocument doc)
    {
        List<XElement> texts = doc.Root.XPath2SelectElements("/page/text[@column]").ToList();
        SortedDictionary<int, Box> boxes = new SortedDictionary<int,Box>();

        foreach(XElement text in texts)
        {
            int column = int.Parse(text.Attribute("column").Value);
            int left = int.Parse(text.Attribute("left").Value);
            int width = int.Parse(text.Attribute("width").Value);
            int right = left + width;

            int top = int.Parse(text.Attribute("top").Value);
            int height = int.Parse(text.Attribute("height").Value);
            int bottom = top + height;

            if (!boxes.ContainsKey(column))
            {
                boxes.Add(column, new Box());
            }

            Box limit = boxes[column];

            limit.Left = Math.Min(limit.Left, left);
            limit.Right = Math.Max(limit.Right, right);
            limit.Top = Math.Min(limit.Top, top);
            limit.Bottom = Math.Max(limit.Bottom, bottom);
        }

        foreach(var kv in boxes)
        {
            int column = kv.Key;
            Box limit = kv.Value;
            List<XElement> heads = doc.Root.XPath2SelectElements($"/page/text[@top<{limit.Top} and @left>{limit.Left-5} and @left+@width<{limit.Right+5}]").ToList();
            foreach(var head in heads)
            {
                head.Add(new XAttribute("column", column));
                head.Add(new XAttribute("head", "true"));
            }
        }

        return doc;
    }
}
