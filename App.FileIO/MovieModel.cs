namespace App.FileIO
{
    public class MovieModel
    {
        public string Name { get; set; }
        public string Score { get; set; }
        public System.Collections.Generic.Dictionary<string, string> Time { get; set; }
        public System.Collections.Generic.List<string> Director { get; set; }
        public System.Collections.Generic.List<string> Actor { get; set; }
        public System.Collections.Generic.List<string> Tag { get; set; }
        public System.Collections.Generic.List<string> Quote { get; set; }
        public System.Collections.Generic.List<string> Share { get; set; }
        public System.Collections.Generic.List<string> Type { get; set; }
        public System.Collections.Generic.List<string> Image { get; set; }
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> Url { get; set; }
    }
}
