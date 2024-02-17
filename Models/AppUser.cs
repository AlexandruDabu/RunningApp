using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GroopWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int Pace { get; set; }
        public int Mileage { get; set; }
        public Address? Address { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Race>? Races { get; set; }
    }
}