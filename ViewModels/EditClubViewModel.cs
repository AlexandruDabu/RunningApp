using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Enum;
using GroopWebApp.Models;

namespace GroopWebApp.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public ClubCategory ClubCategory { get; set; }

    }
}