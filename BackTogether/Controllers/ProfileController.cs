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
        public IActionResult Index() {
            // Show profile page
            // Get all relevant data here using the Service
            _userService.GetUser(5);
            return View();
        }

        // GET: Login
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to login
        [HttpGet]
        public IActionResult Login() {
            // Show Login Form
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password) {

            var uID = _loginService.AuthenticateUser(username, password);

            if (uID != -1) {
                // Success Login
                // Update Session Info
                // Redirect to index
                var isAdmin = _loginService.AuthenticateAdmin(uID);
                if (isAdmin) {
                    // Update session info
                    // Enable Admin functionality
                }
                return RedirectToAction("Index");
            } else {
                // Wrong Credentials 
                // Display appropriate message
                // Redirect to Login
                return RedirectToAction();
            }
        }

        // GET: Profile/Register
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to Login
        [HttpGet]
        public IActionResult Register() {
            // Show the Register Form
            return View();
        }

        // POST: Profile/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id, FullName, Username, Password, Email, ImageURLId, HasAdminPrivileges")] User user) {
            if (ModelState.IsValid) {
               var u = await _userService.CreateUser(user);
            }
            // Setup session info while the user is created asynchronously
            // Using the `u` var

            return RedirectToAction("Index");
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
