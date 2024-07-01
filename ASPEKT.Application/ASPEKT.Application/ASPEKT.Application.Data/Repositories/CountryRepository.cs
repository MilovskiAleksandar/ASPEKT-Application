using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;

namespace ASPEKT.Application.Data.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(Country entity)
        {
            _context.Countries.Add(entity);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
           Country country = _context.Countries.FirstOrDefault(x => x.Id == id);
            if(country == null)
            {
                throw new Exception();
            }

            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

        public List<Country> GetAll()
        {
            return _context.Countries.ToList();
        }

        public Country GetById(int id)
        {
            return _context.Countries.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Country entity)
        {
            _context.Countries.Update(entity);
            _context.SaveChanges();
        }
    }
}
