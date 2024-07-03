using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ASPEKT.Application.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Create(Contact entity)
        {
            _context.Contacts.Add(entity);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Contact contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if(contact == null)
            {
                throw new Exception();
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        public List<Contact> FilterContact(int? countryId, int? companyId)
        {
            if(countryId == null)
            {
                return _context.Contacts.Include(x => x.Company).Where(x => x.CompanyId == companyId).ToList();
            }

            if(companyId == null)
            {
                return _context.Contacts.Include(x => x.Country).Where(x => x.CountryId == countryId).ToList();
            }

            return _context.Contacts.Include(x => x.Company).Include(x => x.Country).Where(x => x.CompanyId == companyId && x.CountryId == countryId).ToList();
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public List<Contact> GetContactsWithCompanyAndCountry()
        {
            return _context.Contacts.Include(x => x.Company).Include(x => x.Country).ToList();
        }

        public void Update(Contact entity)
        {
            _context.Contacts.Update(entity);
            _context.SaveChanges();
        }
    }
}
