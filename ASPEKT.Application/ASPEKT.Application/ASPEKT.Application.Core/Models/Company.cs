

namespace ASPEKT.Application.Core.Models
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public List<Contact> CompanyContacts { get; set; }
    }
}
