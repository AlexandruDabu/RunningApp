using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Data;
using GroopWebApp.Interfaces;
using GroopWebApp.Models;
using GroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepository clubRepository,IPhotoService photoService)
        {
            _photoService = photoService;
            _clubRepository = clubRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid){
               var result = await _photoService.AddPhotoAsync(clubVM.Image);
               
               var club = new Club
               {
                Title=clubVM.Title,
                Description = clubVM.Description,
                Image=result.Url.ToString()
               };
            _clubRepository.Add(club);
            return RedirectToAction("Index");
            }
            else{
                ModelState.AddModelError("","Photo upload failed");
            }
            return View(clubVM);
            
        }

    }
}