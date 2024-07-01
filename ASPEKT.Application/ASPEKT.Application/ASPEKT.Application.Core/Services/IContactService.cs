using ASPEKT.Application.Core.Models;

namespace ASPEKT.Application.Core.Services
{
    public interface IContactService<T, R> : IService<T>
    {
        List<R> FilterByCompanyAndCountry(int? companyId, int? countryId);
        List<R> GetContactWithCompanyAndCountry();
    }
}
