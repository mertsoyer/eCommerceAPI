﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Domain.Entities.Identity
{
    /// <summary>
    /// Microsoft Identity mekanizmasından inherit alıp ekstra identity için propertler ( alanlar ) ekleme işlemleri
    /// </summary>
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
