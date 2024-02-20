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
namespace GroopWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;
        
        private readonly UserManager<AppUser> _userManager;
        
        private readonly ILocationService _locationService;
        private readonly SignInManager<AppUser> _signInManager;
    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository, UserManager<AppUser> userManager, ILocationService locationService, SignInManager<AppUser> signInManager)
    {
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
            }
            return RedirectToAction("Login", "Account");
        }

    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();
        try
        {
            string url = "https://ipinfo.io?token=2d394a16ceb5d1";
            var info = new WebClient().DownloadString(url);
            ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
            RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;
            homeViewModel.City = ipInfo.City;
            homeViewModel.State = ipInfo.Region;
            if(homeViewModel.City!=null)
            {
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
            }
            {
                homeViewModel.Clubs=null;
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
