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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace GroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClubController(IClubRepository clubRepository,IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel
            {
                AppUserId = curUserId
                
            };
            return View(createClubViewModel);
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
                Image=result.Url.ToString(),
                AppUserId = clubVM.AppUserId,
                Address = new Address
                {
                    Street = clubVM.Address.Street,
                    City = clubVM.Address.City,
                    State = clubVM.Address.State
                }
               };
            _clubRepository.Add(club);
            TempData["SuccessClub"] = "You created the club!";
            return RedirectToAction("Index");
            }
            else{
                ModelState.AddModelError("","Photo upload failed");
            }
            return View(clubVM);
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }
        [HttpPost,ActionName("Delete")]

        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            _clubRepository.Delete(clubDetails);
            TempData["DeleteClub"] = "Club deleted successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id){
            var club = await _clubRepository.GetByIdAsync(id);
            if(club==null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Url = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit club");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if(userClub != null){
            try{
                await _photoService.DeletePhotoAsync(userClub.Image);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("","Could not delete the photo");
                return View(clubVM);
            }
            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString() == "/image/user.jpg" ? userClub.Image : photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address,
                AppUserId = userClub.AppUserId
            };
            _clubRepository.Update(club);
            TempData["EditClub"] = "You edited the club succesfully";
            return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }           
        }
    }
}