using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services;
using BackTogether.Services.api;

namespace BackTogether.Controllers {
    public class ProfileController : Controller {

        private readonly ILogin _loginService;
        private readonly IUser _userService;

        public ProfileController(ILogin loginService, IUser userService) {
            _loginService = loginService;
            _userService = userService;
        }

        // GET: Profile
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Show profile
        // * If no -> redirect to login
        [HttpGet]
        public IActionResult Index(int id) {
            // Show profile page
            // Get all relevant data here using the Service
            _userService.GetUser(id);
            return View();
        }

        [HttpGet]
        public IActionResult Backed() {
            // Get all projects backed by user with this ID
            return View();
        }

        [HttpGet]
        public IActionResult Projects() {
            // Get all projects created by user with this ID
            return View();
        }

        // GET: Admin
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to login
        // Then check if he has admin Privileges
        [HttpGet]
        public IActionResult Admin() {
            return RedirectToAction("Index");
        }
    }
}
