using ASPEKT.Application.Core.Models;
using ASPEKT.Application.DTOS.Contact;

namespace ASPEKT.Application.Services.Mappers
{
    public static class ContactMapper
    {
        public static ContactDto ToContactDto(this Contact contact)
        {
            return new ContactDto()
            {
                CompanyId = contact.CompanyId,
                ContactId = contact.Id,
                ContactName = contact.ContactName,
                CountryId = contact.CountryId,
            };
        }

        public static Contact ToContact(this ContactDto contact)
        {
            return new Contact()
            {
                Id = contact.ContactId,
                CompanyId = contact.CompanyId,
                ContactName = contact.ContactName,
                CountryId = contact.CountryId

            };
        }

        public static FilterContactDto ToGetContactCompanyCountry(this Contact getContactCompanyCountry)
        {
            return new FilterContactDto()
            {
                ContactName = getContactCompanyCountry.ContactName,
                CompanyId = getContactCompanyCountry.CompanyId,
                ContactId = getContactCompanyCountry.Id,
                CompanyName = $"{getContactCompanyCountry.Company.CompanyName}",
                CountryName = $"{getContactCompanyCountry.Country.CountryName}",
                CountryId = getContactCompanyCountry.CountryId
            };
        }

        public static FilterContactDto ToFilterContact(this Contact contact)
        {
            return new FilterContactDto()
            {
                ContactName = contact.ContactName,
                CompanyId = contact.CompanyId,
                CountryId = contact.CountryId,
                ContactId = contact.Id,
                CountryName = $"{contact.Country.CountryName}",
                CompanyName = $"{contact.Company.CompanyName}",
            };
        }

        public static FilterContactDto ToContactFilterCompany(this Contact contact)
        {
            return new FilterContactDto()
            {
                ContactName = contact.ContactName,
                CompanyId = contact.CompanyId,
                ContactId = contact.Id,
                CompanyName = $"{contact.Company.CompanyName}"
            };
        }

        public static FilterContactDto ToContactFilterCountry(this Contact contact)
        {
            return new FilterContactDto()
            {
                ContactName = contact.ContactName,
                CompanyId = contact.CompanyId,
                ContactId = contact.Id,
                CountryName = $"{contact.Country.CountryName}",
            };
        }
    }
}
