using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Data.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(Company entity)
        {
            _context.Companies.Add(entity);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Company company = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                throw new Exception();
            }
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public Company GetById(int id)
        {
            return _context.Companies.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Company entity)
        {
            _context.Companies.Update(entity);
            _context.SaveChanges();
        }
    }
}
