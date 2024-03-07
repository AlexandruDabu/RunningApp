using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Data.Enum;
using GroopWebApp.Models;

namespace GroopWebApp.ViewModels
{
    public class EditRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}