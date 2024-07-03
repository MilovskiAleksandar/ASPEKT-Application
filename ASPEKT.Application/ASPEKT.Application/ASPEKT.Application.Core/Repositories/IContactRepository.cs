using ASPEKT.Application.Core.Models;


namespace ASPEKT.Application.Core.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        List<Contact> GetContactsWithCompanyAndCountry();
        List<Contact> FilterContact(int? countryId, int? companyId);
    }
}
