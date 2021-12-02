namespace Aries.Processors
{
    public class Text
    {
        public int Left { get; set; }
        public int Width { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Bottom { get; set; }
        
        public int? Column { get; set; }
        public int? Row { get; set; }
        public bool Number { get; set; }
        public bool Head { get; set; }

        
        public string Content { get; set; }

        public Text(XElement element)
        {
            Left = element.Attribute("left").Value.ToInt();
            Width = element.Attribute("width").Value.ToInt();
            Right = Left + Width;
            Top = element.Attribute("top").Value.ToInt();
            Height = element.Attribute("height").Value.ToInt();
            Bottom = Top + Height;

            Column = element.Attribute("column")?.Value?.ToIntNullable();
            Row = element.Attribute("row")?.Value?.ToIntNullable();
            Number = element.Attribute("number") != null;
            Head = element.Attribute("head") != null;
            Content = element.Value;
        }
    }
}
