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
    }");

        foreach (XElement node in doc.XPathSelectElements("//fontspec"))
        {
            if(node == null) { continue; }
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

        sb.Append(@"    <div style=""position:fixed;height: 50px;z-index: 1000;background-color: beige;"">");
        sb.Append(@$"<a href=""{processId}.xml"">XML</a>");
        sb.Append(@$"Page: ");
        var pages = doc.XPathSelectElements("//page");
        foreach (XElement page in pages)
        {
            string pNo = page.Attribute("number")?.Value;
            sb.Append(@$" <a href=""#page{pNo}"">{pNo}</a> ");
        }

        sb.Append(@"</div><div style=""height: 50px;""></div>");
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
                sb.Append($@"<span class=""text font{fontId}"" style=""top:{top}px;left:{left}px;height:{h}px;width:{w}px"">{v}</span>");
            }
            
            sb.AppendLine("</div>");
        }

        sb.AppendLine("</body></html>");

        XDocument html = XDocument.Parse(sb.ToString());
        

        return html.ToString();
    }
}
