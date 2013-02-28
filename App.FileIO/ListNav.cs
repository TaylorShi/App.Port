namespace App.FileIO
{

    public class ListNav
    {
        public OpenStyle Style { get; set; }
        public LayoutStyle Layout { get; set; }
        public int SecondListLines { get; set; }
        public int LineHeight { get; set; }
        public int LineFloat { get; set; }
        public System.Drawing.Color Line { get; set; }
        public System.Drawing.Color CellColor { get; set; }
        public System.Drawing.Font CellFont { get; set; }
        public int ListFloat { get; set; }
        public System.Drawing.Size ListClose { get; set; }
        public System.Drawing.Size ListOpen { get; set; }
        public System.Drawing.Size IcoSize { get; set; }
        public System.Drawing.Size ListSize { get; set; }
        public System.Drawing.Size CellSize { get; set; }
        public System.Collections.Generic.Dictionary<string, bool> IsSuo { get; set; }
        public System.Collections.Generic.List<System.Drawing.Color> TitleColor { get; set; }
        public System.Collections.Generic.List<System.Drawing.Font> TitleFont { get; set; }
        public System.Collections.Generic.List<FirstNav> First { get; set; }
    }

    public class FirstNav
    {
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public System.Drawing.Color FirstNavColor { get; set; }
        public SecondNav Next { get; set; }
    }

    public class SecondNav
    {
        public System.Collections.Generic.List<string> Seconds { get; set; }
        public ThirdNav Next { get; set; }
    }

    public class ThirdNav
    {
        public System.Collections.Generic.List<string> Thirds { get; set; }
    }

    public enum OpenStyle
    {
        FirstOpen,
        LastOpen,
        AllOpen,
        AllClose
    }

    public enum LayoutStyle
    {
        Horizon,Vertical
    }
}
