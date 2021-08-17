using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }        
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
