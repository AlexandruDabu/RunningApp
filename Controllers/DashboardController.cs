using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using GroopWebApp.Data;
using GroopWebApp.Interfaces;
using GroopWebApp.Models;
using GroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor,IPhotoService photoService)
        {
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _dashboardRepository = dashboardRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
            
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if(user == null) return View("Error");
            var editUserProfile = new EditUserProfileViewModel()
            {
                Id = curUserId,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
                Bio = user.Bio
            };
            return View(editUserProfile);

        }
        private void MapUserEdit(AppUser user, EditUserProfileViewModel editVM)
        {
            user.Id = editVM.Id;
            user.Pace = editVM.Pace;
            user.Mileage = editVM.Mileage;
            user.City = editVM.City;
            user.State = editVM.State;
            user.Bio = editVM.Bio;
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit profile");
                return View("EditUserProfile",editVM);
            }
            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);
                MapUserEdit(user,editVM);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");

        }
    }
}