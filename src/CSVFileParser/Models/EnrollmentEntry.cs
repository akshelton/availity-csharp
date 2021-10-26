namespace CSVFileParser.Models
{
    public class EnrollmentEntry
    {
        public string UserId { get; set; }
        
        public UserName FullName { get; set; }
        
        public int Version { get; set; }
        
        public string InsuranceCompany { get; set; }

        public override string ToString()
        {
            return UserId + "," + FullName + "," + Version + "," + InsuranceCompany;
        }
    }
}