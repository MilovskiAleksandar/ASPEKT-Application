using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.DTOS.Contact;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace ASPEKT.Application.Services.ComapnyServiceUnitTests
{
    [TestClass]
    public class ContactUnitTests
    {
        private readonly ContactService _contactService;
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IRepository<Company>> _mockCompanyRepository;
        private readonly Mock<IRepository<Country>> _mockCountryRepository;

        public ContactUnitTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockCompanyRepository = new Mock<IRepository<Company>>();
            _mockCountryRepository = new Mock<IRepository<Country>>();

            _contactService = new ContactService(_mockCompanyRepository.Object, _mockCountryRepository.Object, _mockContactRepository.Object);
        }

        [TestMethod]
        public void FilterByCompanyAndCountry_CompanyId_ShouldReturnFilteredContacts()
        {
            int? companyId = 1;
            int? countryId = null;

            List<Contact> contacts = new List<Contact>
            {
                new Contact() {Id = 1, ContactName = "Aleksandar", CompanyId = 1, CountryId = 1}
            };

            _mockContactRepository.Setup(x => x.FilterContact(companyId, countryId)).Returns(contacts);

            List<FilterContactDto> filter = _contactService.FilterByCompanyAndCountry(companyId, countryId);

            Assert.AreEqual(1, filter.Count);
            Assert.AreEqual("Aleksandar", filter.First().ContactName);
        }


    }
}
