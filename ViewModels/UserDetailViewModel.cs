using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroopWebApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Pace { get; set; }
        public int Mileage { get; set; }
    }
}