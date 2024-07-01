using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Company;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using ASPEKT.Application.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Services
{
    public class CountryService : IService<CountryDto>
    {
        private IRepository<Country> _countryRepository;
        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public void AddEntity(CountryDto entity)
        {
            ValidateInputForCountry(entity);
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
            ValidateInputForCountry(entity);
            Country countryDb = _countryRepository.GetById(entity.CountryId);
            if( countryDb == null)
            {
                throw new NotFoundException($"Country with id {entity.CountryId} was not found.");
            }

            countryDb.CountryName = entity.CountryName;
            _countryRepository.Update(countryDb);
        }

        private void ValidateInputForCountry(CountryDto entity)
        {
            if (string.IsNullOrEmpty(entity.CountryName))
            {
                throw new WrongDataException("The country name must be entered!!");
            }
            if (entity.CountryName.Length > 50)
            {
                throw new WrongDataException("The country name must be less than a 50 characters");
            }
        }
    }
}
