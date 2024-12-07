namespace VineforceAPIDemoTest.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class CreateCountryDto
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
       
    }

    public class UpdateCountryDto : CreateCountryDto
    {
        public int Id { get; set; }
    }

}
