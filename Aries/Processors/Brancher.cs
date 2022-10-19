namespace Aries.Processors;

[Serializable]
public class Brancher : Processor
{
    public ProcessorContainer FirstProcesses { get; set; }
    public ProcessorContainer SecondProcesses { get; set; }

    public override XDocument Process(XDocument doc)
    {
        return doc;
    }
}
