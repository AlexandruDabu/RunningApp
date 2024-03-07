using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroopWebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Pace { get; set; }
        public int Mileage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Bio { get; set; }
        
    }
}