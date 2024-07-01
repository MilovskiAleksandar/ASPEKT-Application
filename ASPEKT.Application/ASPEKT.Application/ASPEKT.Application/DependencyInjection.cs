using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Repositories;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.Data;
using ASPEKT.Application.Data.Repositories;
using ASPEKT.Application.DTOS.Company;
using Microsoft.EntityFrameworkCore;
using ASPEKT.Application.Services;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.DTOS.Contact;

namespace ASPEKT.Application
{
    public static class DependencyInjection
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Company>, CompanyRepository>();
            services.AddTransient<IRepository<Country>, CountryRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IService<CompanyDto>, CompanyService>();
            services.AddTransient<IService<CountryDto>, CountryService>();
            services.AddTransient<IContactService<ContactDto, FilterContactDto>, ContactService>();
        }
    }
}
