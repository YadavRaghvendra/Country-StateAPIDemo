namespace VineforceAPIDemoTest.DTOs
{

    public class StateDto
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class CreateStateDto
    {
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
    }

    public class UpdateStateDto : CreateStateDto
    {
        public int Id { get; set; }

    }
}
