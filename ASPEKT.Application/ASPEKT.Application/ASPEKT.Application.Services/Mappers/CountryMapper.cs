using ASPEKT.Application.Core.Models;
using ASPEKT.Application.DTOS.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Services.Mappers
{
    public static class CountryMapper
    {
        public static CountryDto ToCountryDto(this Country country)
        {
            return new CountryDto { CountryId = country.Id, CountryName = country.CountryName };
        }

        public static Country ToCountry(this CountryDto country)
        {
            return new Country { CountryName = country.CountryName };
        }
    }
}
