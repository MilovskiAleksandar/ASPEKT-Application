namespace ASPEKT.Application.DTOS.Contact
{
    public class FilterContactDto
    {
        public int ContactId { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string ContactName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }
    }
}
