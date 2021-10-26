namespace CSVFileParser.Models
{
    public class UserName
    {
        public string First { get; set; }
        
        public string Last { get; set; }

        public UserName(string name)
        {
            var parts = name.Split(' ');
            First = parts[0];
            Last = parts[1];
        }

        public override string ToString()
        {
            return First + " " + Last;
        }
    }
}