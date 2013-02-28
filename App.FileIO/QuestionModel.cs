namespace App.FileIO
{
    public class QuestionModel
    {
        public QuestionType QueType { get; set; }
        public string QueTag { get; set; }
        public string QueNum { get; set; }
        public string QueString { get; set; }
        public string QueImage { get; set; }
        public System.Collections.Generic.List<string> QueSelect { get; set; }
        public System.Collections.Generic.List<string> AnsRight { get; set; }
        public string AnsDetail { get; set; }
        public string AnsVideo { get; set; }
        public string AnsImage { get; set; }
    }
}
