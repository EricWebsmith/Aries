namespace Aries.Test;

[TestClass]
public class Xml2HtmlTest
{
    [TestMethod]
    public void TestMethod1()
    {
        string file = @"D:\projects\Aries\sample.xml";
        XDocument doc = XDocument.Load(file);
        string html = Xml2Html.ToHtml(doc, 0);
        Console.WriteLine(html);

        string htmlPath = @"D:\projects\Aries\sample.html";
        File.WriteAllText(htmlPath, html);
    }
}
