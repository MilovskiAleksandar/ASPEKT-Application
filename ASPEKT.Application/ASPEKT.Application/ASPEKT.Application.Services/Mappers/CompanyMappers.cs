using ASPEKT.Application.Core.Models;
using ASPEKT.Application.DTOS.Company;

namespace ASPEKT.Application.Mappers
{
    public static class CompanyMappers
    {
        public static CompanyDto ToCompanyDto(this Company company)
        {
            return new CompanyDto() { CompanyId = company.Id, CompanyName = company.CompanyName };
        }

        public static Company ToCompany(this CompanyDto company)
        {
            return new Company() { CompanyName = company.CompanyName };
        }
    }
}
