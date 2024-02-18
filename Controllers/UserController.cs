using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Interfaces;
using GroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepsotiroy _userRepsotiroy;
        public UserController(IUserRepsotiroy userRepsotiroy)
        {
            _userRepsotiroy = userRepsotiroy;
            
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepsotiroy.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage
                };
                result.Add(userViewModel);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepsotiroy.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage
            };
            return View(userDetailViewModel);
        }
    }
}