namespace Aries;

public static class Xml2Html
{
    const float HoritontalMagnitude = 1.4F;
    const float VerticalMagnitude = 1.2F;

    private static string HtmlEncode(this string s)
    {
        return System.Net.WebUtility.HtmlEncode(s);
    }

    private static string HoritontalMagnify(this string v)
    {
        int i = int.Parse(v);
        float f = i * HoritontalMagnitude;
        int result = (int)f;
        return result.ToString();
    }

    private static string VerticalMagnify(this string v)
    {
        int i = int.Parse(v);
        float f = i * VerticalMagnitude;
        int result = (int)f;
        return result.ToString();
    }

    public static string ToHtml(XDocument doc, int processId)
    {
        switch (doc.Root.Name.ToString())
        {
            case "pdf2xml":
                return ToHtmlMain(doc, processId);
            case "table":
                return ToHtmlTable(doc, processId);
        }
        return null;
    }

    private static string ToHtmlTable(XDocument doc, int processId)
    {
        var cells = doc.Root.Elements("row").Elements("cell").ToList();
        Dictionary<string, int> headDict = new Dictionary<string, int>();
        List<string> heads = new List<string>();
        int headIndex = 1;
        foreach (var cell in cells)
        {
            string head = cell.Attribute("name")?.Value;
            if (headDict.ContainsKey(head))
            {
                continue;
            }
            heads.Add(head);
            headDict.Add(head, headIndex);
            headIndex++;
        }
        int colsCount = headIndex;


        string htmlString = @"
 <html lang = ""en"">
<head>
    <meta charset = ""UTF-8"" />
    <meta http-equiv = ""X-UA-Compatible"" content = ""IE=edge"" />
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1.0"" />
    <title> Document </title>
    <style>
    a{
      padding: 3px;
    }

    .number{
        font-style: italic;
    }
    table{
      border: 1px solid black;
    }

    th{
      border: 1px solid black;
      padding: 5px;
    }

    td{
      border: 1px solid black;
      padding: 5px;
    }
    </style>
</head>
<body>" +
@$"<a href=""{processId}.xml"">XML</a>
<table></table>
</body></html>";

        XDocument html = XDocument.Parse(htmlString);
        XElement table = html.Root.XPath2SelectElement("/body/table");

        XElement headTr = new XElement("tr");
        headTr.Add(new XElement("th"));
        foreach(string head in heads)
        {
            XElement th = new XElement("th");
            th.Value = head;
            headTr.Add(th);
        }
        table.Add(headTr);

        var rows = doc.Root.Elements("row");
        foreach (var row in rows)
        {
            XElement tr = new XElement("tr");
            string rowName = row.Attribute("name")?.Value;
            XElement[] tds = new XElement[colsCount];
            for (int i = 0; i < tds.Length; i++)
            {
                tds[i] = new XElement("td");
            }
            // Array.Fill(tds, new XElement("td"));
            if (rowName != null)
            {
                tds[0].Value = rowName;
            }
            
            foreach(var cell in row.Elements("cell"))
            {
                string cellName = cell.Attribute("name")?.Value;
                if (!headDict.ContainsKey(cellName))
                {
                    continue;
                }

                headIndex = headDict[cellName];
                tds[headIndex].Value = cell.Value;
            }
            tr.Add(tds);
            table.Add(tr);
        }

        return html.ToString();
    }

    private static string ToHtmlMain(XDocument doc, int processId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"
 <html lang = ""en"">
<head>
    <meta charset = ""UTF-8"" />
    <meta http-equiv = ""X-UA-Compatible"" content = ""IE=edge"" />
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1.0"" />
    <title> Document </title>
    <style>
    a{
      padding: 3px;
    }
    .page{
      position: relative;
      border: 1px solid black;
    }

    .text{
      position: absolute;
      border: 1px solid black;
    }

    .number{
        font-style: italic;
    }

    .column0{
      background-color: #8b6b6b;
    }

    .column1{
      background-color:rgb(86, 105, 84);
    }

    .column2{
      background-color: rgb(93, 109, 160);
    }

    .column3{
      background-color:rgb(165, 153, 112);
    }

    .column4{
      background-color:rgb(137, 97, 160);
    }

    .row0{
      color:rgb(255, 9, 9);
    }

    .row1{
      color:rgb(10, 255, 9);
    }

    .row2{
      color:rgb(6, 11, 255);
    }

    .row3{
      color:rgb(255, 255, 6);
    }
");

        foreach (XElement node in doc.XPathSelectElements("//fontspec"))
        {
            if (node == null) { continue; }
            string id = node.Attribute("id").Value;
            string size = node.Attribute("size").Value;
            sb.Append(@$"
    .font{id}
    {{
      font-size: {size}px;
    }}
");
        }

        sb.Append(@"</style>
</head>
<body>
");

        sb.Append(@"    <div style=""position:fixed;height: 30px;z-index: 1000;background-color: beige;"">");
        sb.Append(@$"<a href=""{processId}.xml"">XML</a>");
        sb.Append(@$"Page: ");
        var pages = doc.XPathSelectElements("//page");
        foreach (XElement page in pages)
        {
            string pNo = page.Attribute("number")?.Value;
            sb.Append(@$" <a href=""#page{pNo}"">{pNo}</a> ");
        }

        sb.Append(@"</div><div style=""height: 30px;""></div>");
        foreach (XElement node in pages)
        {
            string pNo = node.Attribute("number")?.Value;
            sb.Append($@"<a id=""page{pNo}"" />");
            sb.Append($@"<h1>Page {pNo}</h1>");

            string pH = node.Attribute("height")?.Value.VerticalMagnify();
            string pW = node.Attribute("width")?.Value.HoritontalMagnify();

            sb.Append($@"<div class=""page"" style=""height:{pH}px;");
            sb.Append($@"width:{pW}px"">");

            var texts = node.XPathSelectElements(@"text");
            foreach (var text in texts)
            {
                string top = text.Attribute("top").Value.VerticalMagnify();
                string left = text.Attribute("left").Value.HoritontalMagnify();
                string h = text.Attribute("height").Value.VerticalMagnify();
                string w = text.Attribute("width").Value.HoritontalMagnify();
                string v = System.Net.WebUtility.HtmlEncode(text.Value);
                string fontId = text.Attribute("font").Value;
                string title = text.ToString().HtmlEncode();
                string columnId = text.Attribute("column")?.Value;
                string columnClass = string.IsNullOrWhiteSpace(columnId) ? string.Empty : $"column{columnId}";
                string rowId = text.Attribute("row")?.Value;
                string rowClass = string.Empty;
                if (!string.IsNullOrWhiteSpace(rowId))
                {
                    int row = int.Parse(rowId);
                    row = row % 4;
                    rowClass = $"row{row}";
                }

                string number = text.Attribute("number")?.Value == "true" ? "number" : "";

                sb.Append($@"<span class=""text font{fontId} {columnClass} {rowClass} {number}"" style=""top:{top}px;left:{left}px;height:{h}px;width:{w}px"" title=""{title}"">{v}</span>");
            }

            sb.AppendLine("</div>");
        }

        sb.AppendLine("</body></html>");

        XDocument html = XDocument.Parse(sb.ToString());


        return html.ToString();
    }
}
