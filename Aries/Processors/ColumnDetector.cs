namespace Aries.Processors;



[Serializable]
public class ColumnDetector : Processor
{

    record PositionedText: IComparable<PositionedText>
    {
        public int X { get; set; }
        public bool IsStart { get; set; }
        public XElement Text { get; set; }

        public int CompareTo(PositionedText other)
        {
            return this.X.CompareTo(other.X);
        }
    }

    public override XDocument Process(XDocument doc)
    {
        var pages = doc.Root.Elements("page").ToList();
        foreach (var page in pages)
        {
            var texts = page.XPath2SelectElements("text[@number='true']").ToList();
            
            var ptexts = new List<PositionedText>();
            foreach(var text in texts)
            {
                int left = int.Parse(text.Attribute("left")?.Value);
                int width = int.Parse(text.Attribute("width")?.Value);
                int start = left;
                int end = left + width;
                ptexts.Add(new PositionedText(){X=start, IsStart=true, Text=text });
                ptexts.Add(new PositionedText() { X = end, IsStart = false, Text = text });
            }
            ptexts.Sort();

            int groupId = -1;
            int cellCount = 0;
            foreach(var ptext in ptexts)
            {
                if (cellCount == 0)
                {
                    groupId++;
                }

                if(ptext.IsStart)
                {
                    cellCount++;
                    ptext.Text.Add(new XAttribute("column", groupId));
                }
                else
                {
                    cellCount--;
                }
            }
        }
        return doc;
    }

    private int Find(int[] unionFind, int i)
    {
        int p = i;
        while(p != unionFind[p])
        {
            p = unionFind[p];
        }
        return p;
    }

    private void Union(int[] unionFind, int i, int j)
    {
        int p1 = Find(unionFind, i);
        int p2 = Find(unionFind, j);
        unionFind[p2] = p1;
    }

    private void Flatten(int[] unionFind)
    {
        for(int i = 0;i < unionFind.Length; i++)
        {
            unionFind[i] = Find(unionFind, i);
        }
    }
}

