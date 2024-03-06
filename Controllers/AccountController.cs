using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Data;
using GroopWebApp.Models;
using GroopWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
        
        //User is found, check password
        if(user != null){
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if(passwordCheck)
            {
                //Password correct
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if(result.Succeeded)
                {
                    TempData["SuccessLogin"] = "You logged in with success";
                    return RedirectToAction("Index", "Home");
                }
            }
            //Password is incorrect
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginViewModel);
        }
        //User not found
        TempData["Error"] ="Wrong credentials. Please, try again";
        return View(loginViewModel);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> LoginAsUser()
        {
            var result = await _signInManager.PasswordSignInAsync("app-user", "Coding@1234?", false, lockoutOnFailure: false);
            
            if(result.Succeeded)
            {
                TempData["SuccessLogin"] = "You logged in as User!";
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginAsAdmin()
        {
            var result = await _signInManager.PasswordSignInAsync("AlexDab", "Coding@1234?", false, lockoutOnFailure: false);
            
            if(result.Succeeded)
            {
                TempData["AdminSucceed"] = "Logged in as Admin";
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user!=null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            var username = await _userManager.FindByNameAsync(registerViewModel.Username);
            if(username!=null)
            {
                TempData["Error"] = "This username is taken";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                TempData["SuccessRegister"] = "You registered with success";
            }
            else
            {
                TempData["FailRegister"] = "Something went wrong, try again";
            }
            return RedirectToAction("Index","Home");

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessLogOut"] = "You Logged out";
            return RedirectToAction("Index","Home");
        }
    }
}