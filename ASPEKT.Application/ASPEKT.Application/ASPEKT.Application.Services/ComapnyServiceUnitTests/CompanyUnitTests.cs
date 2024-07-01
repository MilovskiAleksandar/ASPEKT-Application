using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.DTOS.Company;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ASPEKT.Application.Services.ComapnyServiceUnitTests
{
    [TestClass]
    public class CompanyUnitTests
    {
        private readonly Mock<IRepository<Company>> _mockCompanyRepository;
        private readonly CompanyService _companyService;

        public CompanyUnitTests()
        {
            _mockCompanyRepository = new Mock<IRepository<Company>>();
            _companyService = new CompanyService(_mockCompanyRepository.Object);
        }

        [TestMethod]
        public void AddEntity_ShouldThrowException_WhenCompanyNameIsAbove50Characters()
        {
            CompanyDto company = new CompanyDto { CompanyName = new string('a', 51) };

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() => _companyService.AddEntity(company));

            Assert.AreEqual("The company name must be less than a 50 characters", exception.Message);
        }

        [TestMethod]
        public void DeleteEntity_ShouldThrowException_WhenIdIsLessOrEqualToZero()
        {
            int id = 0;

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() => _companyService.DeleteEntity(id));
            Assert.AreEqual("ID must be a positive number", exception.Message);
        }

        [TestMethod]
        public void DeleteEntity_ShouldThowException_WhenTheCompanyWithIdDoesntExist()
        {
            int id = 2;
            _mockCompanyRepository.Setup(x => x.GetById(id)).Returns((Company)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _companyService.DeleteEntity(id));
            Assert.AreEqual($"Company with id {id} was not found and cannot be deleted", exception.Message);
        }

        [TestMethod]
        public void GetAll_ShouldThrowException_WhenCompaniesDoesntExist()
        {
            _mockCompanyRepository.Setup(x => x.GetAll()).Returns((List<Company>)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _companyService.GetAll());
            Assert.AreEqual("The database is empty", exception.Message);
        }

        [TestMethod]
        public void GetAll_ShouldReturnCompanies_WhenCompaniesExist()
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id = 1, CompanyName = "ASPEKT"},
                new Company{Id = 2, CompanyName = "Apple"}
            };

            _mockCompanyRepository.Setup(x => x.GetAll()).Returns(companies);

            List<CompanyDto> companies1 = _companyService.GetAll();
            Assert.AreEqual(2, companies1.Count);
        }

        [TestMethod]
        public void GetById_ShouldThrowException_WhenCompanyWithThatIdIsNotFound()
        {
            int id = 1;
            _mockCompanyRepository.Setup(x => x.GetById(id)).Returns((Company)null);

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _companyService.GetById(id));
            Assert.AreEqual($"Company with id {id} was not found", exception.Message);
        }

        [TestMethod]
        public void GetById_ShouldReturnCompany_WhenCompanyWithThatIdIsFound()
        {
            Company company = new Company { Id = 1, CompanyName = "Test" };
            _mockCompanyRepository.Setup(x => x.GetById(company.Id)).Returns((Company)company);

            CompanyDto result = _companyService.GetById(company.Id);

            Assert.AreEqual(company.Id, result.CompanyId);
            Assert.AreEqual(company.CompanyName, result.CompanyName);
        }

        [TestMethod]
        public void GetById_ShouldReturnException_WhenTheIdLessOrEqualToZero()
        {
            int id = 0;

            NotFoundException exception = Assert.ThrowsException<NotFoundException>(() => _companyService.GetById(id));
            Assert.AreEqual("ID cannot be less or equal than 0", exception.Message);
        }

        [TestMethod]
        public void UpdateEntity_ShouldUpdateCompany_WhenCompanyWithThatIdExist()
        {
            Company company = new Company { Id = 1, CompanyName = "Test" };
            CompanyDto companyDto = new CompanyDto { CompanyId = 1, CompanyName = "Test2222" };

            _mockCompanyRepository.Setup(r => r.GetById(company.Id)).Returns(company);

            _companyService.UpdateEntity(companyDto);

            _mockCompanyRepository.Verify(r => r.Update(It.Is<Company>(c =>
                c.Id == companyDto.CompanyId &&
                c.CompanyName == companyDto.CompanyName)), Times.Once);
        }

        [TestMethod]
        public void UpdateEntity_ShouldThrowException_WhenCompanyNameIsInvalid()
        {
            CompanyDto companyDto = new CompanyDto { CompanyId = 1, CompanyName = "" };

            WrongDataException exception = Assert.ThrowsException<WrongDataException>(() =>  _companyService.UpdateEntity(companyDto));
            Assert.AreEqual("The company name must be entered!!", exception.Message);
        }
    }
}
