

namespace ASPEKT.Application.Core.Models
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }

        public List<Contact> CountryContacts { get; set; }
    }
}
