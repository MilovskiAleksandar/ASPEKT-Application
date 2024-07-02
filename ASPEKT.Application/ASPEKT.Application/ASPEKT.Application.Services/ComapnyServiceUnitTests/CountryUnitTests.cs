using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace ASPEKT.Application.Services.ComapnyServiceUnitTests
{
    [TestClass]
    public class CountryUnitTests
    {
        private readonly Mock<IRepository<Country>> _mockCountryRepository;
        private readonly CountryService _countryService;

        public CountryUnitTests()
        {
            _mockCountryRepository = new Mock<IRepository<Country>>();
            _countryService = new CountryService(_mockCountryRepository.Object);
        }

        [TestMethod]
        public void AddEntity_ShouldThrowException_WhenCountryNameIsAbove50Characters()
        {
            CountryDto company = new CountryDto { CountryName = new string('a', 51) };

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() => _countryService.AddEntity(company));

            Assert.AreEqual("The country name must be less than a 50 characters", exception.Message);
        }

        [TestMethod]
        public void DeleteEntity_ShouldThrowException_WhenIdIsLessOrEqualToZero()
        {
            int id = 0;

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() => _countryService.DeleteEntity(id));
            Assert.AreEqual("ID must be a positive number", exception.Message);
        }

        [TestMethod]
        public void DeleteEntity_ShouldThowException_WhenTheCountryWithIdDoesntExist()
        {
            int id = 2;
            _mockCountryRepository.Setup(x => x.GetById(id)).Returns((Country)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _countryService.DeleteEntity(id));
            Assert.AreEqual($"Country with id {id} was not found and cannot be deleted", exception.Message);
        }

        [TestMethod]
        public void GetAll_ShouldThrowException_WhenCountriesDoesntExist()
        {
            _mockCountryRepository.Setup(x => x.GetAll()).Returns((List<Country>)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _countryService.GetAll());
            Assert.AreEqual("The database is empty", exception.Message);
        }

        [TestMethod]
        public void GetAll_ShouldReturnCompanies_WhenCountriesExist()
        {
            List<Country> companies = new List<Country>
            {
                new Country {Id = 1, CountryName = "Macedonia"},
                new Country{Id = 2, CountryName = "Germany"}
            };

            _mockCountryRepository.Setup(x => x.GetAll()).Returns(companies);

            List<CountryDto> companies1 = _countryService.GetAll();
            Assert.AreEqual(2, companies1.Count);
        }

        [TestMethod]
        public void GetById_ShouldThrowException_WhenCountryyWithThatIdIsNotFound()
        {
            int id = 1;
            _mockCountryRepository.Setup(x => x.GetById(id)).Returns((Country)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _countryService.GetById(id));
            Assert.AreEqual($"Country with id {id} was not found", exception.Message);
        }

        [TestMethod]
        public void GetById_ShouldReturnCountry_WhenCountryWithThatIdIsFound()
        {
            Country country = new Country { Id = 1, CountryName = "Test" };
            _mockCountryRepository.Setup(x => x.GetById(country.Id)).Returns((Country)country);

            CountryDto result = _countryService.GetById(country.Id);

            Assert.AreEqual(country.Id, result.CountryId);
            Assert.AreEqual(country.CountryName, result.CountryName);
        }

        [TestMethod]
        public void GetById_ShouldReturnException_WhenTheIdLessOrEqualToZero()
        {
            int id = 0;

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _countryService.GetById(id));
            Assert.AreEqual("ID cannot be less or equal than 0", exception.Message);
        }

        [TestMethod]
        public void UpdateEntity_ShouldUpdateCountry_WhenCountryWithThatIdExist()
        {
            Country country = new Country { Id = 1, CountryName = "Test" };
            CountryDto countryDto = new CountryDto { CountryId = 1, CountryName = "Test2222" };

            _mockCountryRepository.Setup(r => r.GetById(country.Id)).Returns(country);

            _countryService.UpdateEntity(countryDto);

            _mockCountryRepository.Verify(r => r.Update(It.Is<Country>(c =>
                c.Id == countryDto.CountryId &&
                c.CountryName == countryDto.CountryName)), Times.Once);
        }

        [TestMethod]
        public void UpdateEntity_ShouldThrowException_WhenCountryNameIsInvalid()
        {
            CountryDto countryDto = new CountryDto { CountryId = 1, CountryName = "" };

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() => _countryService.UpdateEntity(countryDto));
            Assert.AreEqual("The country name must be entered!!", exception.Message);
        }
    }
}
