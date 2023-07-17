using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services;
using BackTogether.Services.api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BackTogether.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;
        private readonly ILogin _loginService;
        private readonly IDatabase _dbService;

        public HomeController(ILogger<HomeController> logger, ILogin loginService, IDatabase dbService) {
            _logger = logger;
            _loginService = loginService;
            _dbService = dbService;
        }

        public IActionResult Index() {
            // Show Home page
            // One button "Home"
            // One button "Profile"
            // One button "Projects"
            // One button "Create"
            return View();
        }

        public IActionResult Profile() {
            return RedirectToAction("Login");
        }

        public IActionResult Create() {
            return RedirectToAction("Create", "Project");
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

            if (uID == -1) {
                // Wrong Credentials 
                // Display appropriate message
                // Redirect to Login
                Problem("Wrong credentials / User doesnt exist");
                return RedirectToAction("Login");
            }

            // Success Login
            // Update Session Info
            // Redirect to index
            var isAdmin = _loginService.AuthenticateAdmin(uID);
            if (isAdmin) {
                // Update session info
                // Enable Admin functionality
            }
            return RedirectToAction("Index", "Profile", new { id = uID });
        }

        // GET: Home/Register
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to Login
        [HttpGet]
        public IActionResult Register() {
            // Show the Register Form
            return View();
        }

        // POST: Home/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Id, FullName, Username, Password, Email, ImageURLId, HasAdminPrivileges")] User user) {
            if (ModelState.IsValid) {
                var u = _dbService.CreateUser(user);
            }
            // Setup session info while the user is created asynchronously
            // Using the `u` var

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
