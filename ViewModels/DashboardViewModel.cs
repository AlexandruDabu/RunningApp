using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Models;

namespace GroopWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }
    }
}