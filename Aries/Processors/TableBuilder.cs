namespace Aries.Processors;

[Serializable]
public class TableBuilder : Processor
{
    public override XDocument Process(XDocument doc)
    {
        List<Text> texts = new List<Text>();
        foreach(var text in doc.Root.Elements("page").Elements("text"))
        {
            texts.Add(new Text(text));
        }

        // find heads
        var heads = from t in texts where t.Head select t;
        
        Dictionary<int, string> headDict = new Dictionary<int, string>();
        
        foreach(var head in heads)
        {
            headDict[head.Column.Value] = head.Content?.ToLower();
        }

        var groups = from t in texts orderby t.Row group t by t.Row;

        XDocument tableDocument = new XDocument(new XElement("table"));
        foreach (var group in groups)
        {
            XElement row = new XElement("row");
            // group.Key

            bool hasName = false;
            bool hasValue = false;
            foreach(var t in group)
            {

                if (t.Column.HasValue)
                {
                    XElement cell = new XElement("cell");
                    cell.Add(new XAttribute("name", headDict[t.Column.Value]));

                    if (t.Number)
                    {
                        cell.Value = t.Content;
                        hasValue = true;
                    }

                    if(t.Content!= null)
                    {
                        
                        
                    }
                    
                    row.Add(cell);
                }
                else
                {
                    row.Add(new XAttribute("name", t.Content.ToLower()));
                    hasName = true;
                }

            }

            if(hasName || hasValue)
            {
                tableDocument.Root.Add(row);
            }
            
        }

        return tableDocument;
    }
}

