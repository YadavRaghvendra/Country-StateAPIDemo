namespace VineforceAPIDemoTest.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public ICollection<State> States { get; set; } = new List<State>();
    }
}
