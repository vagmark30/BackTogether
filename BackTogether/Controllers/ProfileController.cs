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
        private readonly IDatabase _dbService;

        public ProfileController(ILogin loginService, IDatabase dbService) {
            _loginService = loginService;
            _dbService = dbService;
        }

        // GET: Profile
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Show profile
        // * If no -> redirect to login
        [HttpGet]
        public IActionResult Index() {
			// Show profile page
			// Get all relevant data here using the Service
			return View(_dbService.GetUserById(HttpContext.Session.GetInt32("SessionUserId")));
        }

        [HttpGet]
        public IActionResult Backed(int id) {
            // Get all projects backed by user with this ID
            return View(_dbService.GetBackedProjectsByUserId(id));
        }

        [HttpGet]
        public IActionResult Projects(int id) {
            // Get all projects created by user with this ID
            return View(_dbService.GetCreatedProjectsByUserId(id));
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
