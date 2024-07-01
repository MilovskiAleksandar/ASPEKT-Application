using ASPEKT.Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Core.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        List<Contact> GetContactsWithCompanyAndCountry();
        List<Contact> FilterContact(int? countryId, int? companyId);
    }
}
