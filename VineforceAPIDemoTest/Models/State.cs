namespace VineforceAPIDemoTest.Models
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; } 
        public Country Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
