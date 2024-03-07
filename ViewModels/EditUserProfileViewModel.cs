using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroopWebApp.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        public int Pace { get; set; }
        public int Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IFormFile Image { get; set; }
        public string EmailAddress { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
    }
}