using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Contact;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using ASPEKT.Application.Services.Mappers;
using Microsoft.Extensions.Logging;

namespace ASPEKT.Application.Services
{
    public class ContactService : IContactService<ContactDto, FilterContactDto>
    {
        public IContactRepository _contactRepository;
        public IRepository<Company> _companyRepository;
        public IRepository<Country> _countryRepository;

        public ContactService(IRepository<Company> companyRepository, IRepository<Country> countryRepository, IContactRepository contactRepository)
        {
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _contactRepository = contactRepository;
        }
        public void AddEntity(ContactDto entity)
        {
            ValidateInputForContact(entity);
           Contact newContact = entity.ToContact();
            if(entity == null)
            {
                throw new WrongDataException("You must send data");
            }
            if(entity.CompanyId <= 0)
            {
               throw new WrongDataException("You must send positive data for companyId");
            }
            if(entity.CountryId <= 0)
            {
                throw new WrongDataException("You must send positive data for countryId");
            }
            _contactRepository.Create(newContact);
        }

        public void DeleteEntity(int id)
        {
            if (id <= 0)
            {
                throw new WrongDataException("ID must be a positive number");
            }
            Contact contactDb = _contactRepository.GetById(id);
            if(contactDb == null)
            {
                throw new NotFoundException($"Contact with id {id} was not found and cannot be deleted");
            }
            _contactRepository.Delete(contactDb.Id);
        }

        public List<FilterContactDto> FilterByCompanyAndCountry(int? companyId, int? countryId)
        {
            if (countryId == null)
            {
                List<Contact> contactFirst = _contactRepository.FilterContact(countryId, companyId);
                if(!contactFirst.Any())
                {
                    throw new NotFoundException($"Contacts for Company with id {companyId} not found");
                }
                return contactFirst.Select(x => x.ToContactFilterCompany()).ToList();
            }

            if (companyId == null)
            {
                List<Contact> contactsSecond = _contactRepository.FilterContact(countryId, companyId);
                if (!contactsSecond.Any())
                {
                    throw new NotFoundException($"Contacts for Country with id {countryId} not found");
                }
                return contactsSecond.Select(x => x.ToContactFilterCountry()).ToList();
            }

            List<Contact> contactDb = _contactRepository.FilterContact(countryId, companyId);
            if(!contactDb.Any())
            {
                throw new NotFoundException($"Cannot find contacts for  Country with id {countryId} and Company with id {companyId}");
            }
            return contactDb.Select(x => x.ToFilterContact()).ToList();
        }

        public List<ContactDto> GetAll()
        {
            List<Contact> contactDb = _contactRepository.GetAll();
            if(contactDb == null)
            {
                throw new NotFoundException("The database is empty");
            }
            return contactDb.Select(x => x.ToContactDto()).ToList();
        }

        public ContactDto GetById(int id)
        {
            if(id <= 0)
            {
                throw new NotFoundException("ID cannot be less or equal than 0");
            }
            Contact contactDb = _contactRepository.GetById(id);
            if (contactDb == null)
            {
                throw new NotFoundException($"Contact with id {id} was not found");
            }
            return contactDb.ToContactDto();
        }

        public List<FilterContactDto> GetContactWithCompanyAndCountry()
        {
            List<Contact> contacts = _contactRepository.GetContactsWithCompanyAndCountry();
            if(contacts == null)
            {
                throw new NotFoundException("Cannot get contacts for company and country");
            }
            return contacts.Select(x => x.ToGetContactCompanyCountry()).ToList();
        }

        public void UpdateEntity(ContactDto entity)
        {
            ValidateInputForContact(entity);
            if (entity.CountryId <= 0)
            {
                throw new NotFoundException($"Country with id {entity.CountryId} was not found.");
            }
            if (entity.CompanyId <= 0)
            {
                throw new NotFoundException($"Company with id {entity.CompanyId} was not found.");
            }

            Contact contactDb = _contactRepository.GetById(entity.ContactId);
            if (contactDb == null)
            {
                throw new WrongDataException($"Contact with id {entity.ContactId} was not found.");
            }


            contactDb.ContactName = entity.ContactName;
            contactDb.CountryId = entity.CountryId;
            contactDb.CompanyId = entity.CompanyId;

            _contactRepository.Update(contactDb);
        }

        private void ValidateInputForContact(ContactDto entity)
        {
            if (string.IsNullOrEmpty(entity.ContactName))
            {
                throw new WrongDataException("The contact name must be entered!!");
            }
            if (entity.ContactName.Length > 50)
            {
                throw new WrongDataException("The contact name must be less than a 50 characters");
            }
        }
    }
}
