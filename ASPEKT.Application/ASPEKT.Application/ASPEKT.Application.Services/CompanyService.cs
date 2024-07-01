using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Company;
using ASPEKT.Application.DTOS.Contact;
using ASPEKT.Application.Mappers;
using ASPEKT.Application.Services.Exceptions;

namespace ASPEKT.Application.Services
{
    public class CompanyService : IService<CompanyDto>
    {
        private IRepository<Company> _companyRepository;
        public CompanyService(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public void AddEntity(CompanyDto entity)
        {
            ValidateInputForCompany(entity);
            Company addCompany = entity.ToCompany();
            _companyRepository.Create(addCompany);
        }

        public void DeleteEntity(int id)
        {
            if(id <= 0)
            {
                throw new WrongDataException("ID must be a positive number");
            }
            Company company = _companyRepository.GetById(id);

            if (company == null)
            {
                throw new NotFoundException($"Company with id {id} was not found and cannot be deleted");
            }

            _companyRepository.Delete(company.Id);
        }

        public List<CompanyDto> GetAll()
        {
            List<Company> companies = _companyRepository.GetAll();
            if (companies == null)
            {
                throw new NotFoundException("The database is empty");
            }
            return companies.Select(x => x.ToCompanyDto()).ToList();

        }

        public CompanyDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new NotFoundException("ID cannot be less or equal than 0");
            }
            Company company = _companyRepository.GetById(id);
            if (company == null)
            {
                throw new NotFoundException($"Company with id {id} was not found");
            }

            return company.ToCompanyDto();
        }

        public void UpdateEntity(CompanyDto entity)
        {
            ValidateInputForCompany(entity);
            Company company = _companyRepository.GetById(entity.CompanyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with id {entity.CompanyId} was not found.");
            }

            company.CompanyName = entity.CompanyName;
            _companyRepository.Update(company);
        }

        private void ValidateInputForCompany(CompanyDto entity)
        {
            if (string.IsNullOrEmpty(entity.CompanyName))
            {
                throw new WrongDataException("The company name must be entered!!");
            }
            if(entity.CompanyName.Length > 50)
            {
                throw new WrongDataException("The company name must be less than a 50 characters");
            }
        }
    }
}
