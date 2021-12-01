namespace Aries.Processors;

[Serializable]
public class If : Processor
{
    public string ConditionXPath { get; set; }

    public ProcessorContainer TreeProcesses { get; set; }
    public ProcessorContainer FalseProcesses { get; set; }

    public override XDocument Process(XDocument doc)
    {
        return doc;
    }
}
