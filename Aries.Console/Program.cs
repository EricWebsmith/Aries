// See https://aka.ms/new-console-template for more information

using Aries;
using Aries.Processors;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;

XDocument doc = XDocument.Load(@"D:\projects\Aries\sample.xml");

ProcessorContainer container = new ProcessorContainer();


Filter filter = new Filter();
filter.XPath = "//page[@number='1']";
filter.XPath = @"//text[fn:matches(""..."", ""^\.*$"")]";

string fn = "http://www.w3.org/2005/xpath-functions";
string xpath = @"//text[fn:matches(""..."", ""^\.*$"")]";

// XmlNamespaceResolver 

var namespaceResolver = new XmlNamespaceManager(new NameTable());
namespaceResolver.AddNamespace("fn", fn);

//var nodes = doc.XPath2SelectElements(xpath, namespaceResolver);




Keeper keeper = new Keeper();
keeper.XPath = "//page/text[number(@top)<200 and text()='Statement of Assets and Liabilities']/ancestor::page";
container.Add(keeper);

var json = JsonSerializer.Serialize(container);

File.WriteAllText(@"D:\projects\Aries\sample.json", json);

// doc = filter.Process(doc);
// doc = keeper.Process(doc);

Console.WriteLine("Hello, World!");

