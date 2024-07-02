using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using ASPEKT.Application.Services.FluentValidations;
using ASPEKT.Application.Services.Mappers;
using FluentValidation;

namespace ASPEKT.Application.Services
{
    public class CountryService : IService<CountryDto>
    {
        private IRepository<Country> _countryRepository;
        private CountryValidator _countryValidator;
        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
            _countryValidator = new CountryValidator();
        }
        public void AddEntity(CountryDto entity)
        {
            var validate = _countryValidator.Validate(entity);
            if (!validate.IsValid)
            {
                throw new ValidationException(validate.Errors);
            }
            Country newCountry = entity.ToCountry();
            _countryRepository.Create(newCountry);
        }

        public void DeleteEntity(int id)
        {
            if (id <= 0)
            {
                throw new WrongDataException("ID must be a positive number");
            }
            Country countryDb = _countryRepository.GetById(id);
            if(countryDb == null)
            {
                throw new NotFoundException($"Country with id {id} was not found and cannot be deleted");
            }
            _countryRepository.Delete(countryDb.Id);
        }

        public List<CountryDto> GetAll()
        {
            List<Country> countryDb = _countryRepository.GetAll();
            if(countryDb == null)
            {
                throw new NotFoundException("The database is empty");
            }
            return countryDb.Select(x => x.ToCountryDto()).ToList();
        }

        public CountryDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new NotFoundException("ID cannot be less or equal than 0");
            }
            Country countryDb = _countryRepository.GetById(id);
            if(countryDb == null)
            {
                throw new NotFoundException($"Country with id {id} was not found");
            }

            return countryDb.ToCountryDto();
        }

        public void UpdateEntity(CountryDto entity)
        {
            var validate = _countryValidator.Validate(entity);
            if (!validate.IsValid)
            {
                throw new ValidationException(validate.Errors);
            }
            Country countryDb = _countryRepository.GetById(entity.CountryId);
            if( countryDb == null)
            {
                throw new NotFoundException($"Country with id {entity.CountryId} was not found.");
            }

            countryDb.CountryName = entity.CountryName;
            _countryRepository.Update(countryDb);
        }
    }
}
