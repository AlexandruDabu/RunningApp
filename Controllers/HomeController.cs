using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GroopWebApp.Models;
using GroopWebApp.Interfaces;
using GroopWebApp.Helpers;
using GroopWebApp.ViewModels;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using GroopWebApp.Data;
using GroopWebApp.Interfaces;
using GroopWebApp.Services;
namespace GroopWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;
        
        private readonly UserManager<AppUser> _userManager;
        
        private readonly ILocationService _locationService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGeoLocation _geoLocation;

    public HomeController(ILogger<HomeController> logger, IGeoLocation geoLocation, IClubRepository clubRepository, UserManager<AppUser> userManager, ILocationService locationService, SignInManager<AppUser> signInManager)
    {
        _geoLocation = geoLocation;
        _signInManager = signInManager;
        _locationService = locationService;
        _clubRepository = clubRepository;
        _logger = logger;
        _userManager = userManager;
    }
    
    
        public IActionResult Register() 
        {
            var response = new HomeUserCreateViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var createVM = homeVM.Register;

            if (!ModelState.IsValid) return View(homeVM);

            var user = await _userManager.FindByEmailAsync(createVM.Email);

            if (user != null)
            {
                ModelState.AddModelError("Register.Email", "This email address is already in use");
                return View(homeVM);
            }
            var username = await _userManager.FindByNameAsync(createVM.UserName);
            if(username!=null)
            {
                ModelState.AddModelError("Register.Username", "This username is already taken");
                return View(homeVM);
            }
            var userCity = createVM.City;
            var userState = createVM.State;

            if (userCity == null)
            {
                ModelState.AddModelError("Register.City", "Could not find this City!");
                return View(homeVM);
            }

            var newUser = new AppUser
            {
                UserName = createVM.UserName,
                Email = createVM.Email,
                Address = new Address()
                {
                    State = userState,
                    City = userCity
                }
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, createVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                TempData["SuccessRegister"] = "You registered with success";
            }
            return RedirectToAction("Index", "Home");
        }

    public async Task<IActionResult> Index()
    {
        var homeViewModel = new HomeViewModel();
        try
        {
            
            var result = await _geoLocation.GetGeoInfo();
            GeoLocationViewModel geoLoc = new GeoLocationViewModel();
            geoLoc = JsonConvert.DeserializeObject<GeoLocationViewModel>(result);
            homeViewModel.City = geoLoc.City;
            homeViewModel.State = geoLoc.RegionName;
            homeViewModel.Country = geoLoc.CountryName;
            if(homeViewModel.City!=null)
            {
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
            }
            return View(homeViewModel);
        }
        catch (Exception ex)
        {
            homeViewModel.Clubs=null;
        }
        return View(homeViewModel);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}
