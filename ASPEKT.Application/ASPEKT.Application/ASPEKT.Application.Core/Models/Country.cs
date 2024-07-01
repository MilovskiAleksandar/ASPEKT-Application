using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Core.Models
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }

        public List<Contact> CountryContacts { get; set; }
    }
}
