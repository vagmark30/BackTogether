using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services;
using BackTogether.Services.api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			if (HttpContext.Session.GetInt32("SessionUserId") != null) {
                //If you get here α user is logged in
                ViewData["LoggedIn"] = true;
                return View();
            }
            return View();
        }

        public IActionResult Profile() {
            if (HttpContext.Session.GetInt32("SessionUserId") != null) {
                //If you get here α user is logged in
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Login");
        }

        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Create
        // * If no -> Redirect to login
        public IActionResult Create() {
            if (HttpContext.Session.GetInt32("SessionUserId") != null) {
                //If you get here α user is logged in
                return RedirectToAction("Create", "Project", new { id = HttpContext.Session.GetInt32("SessionUserId") });
            }
            return RedirectToAction("Login");
        }

        // GET: Login
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to login
        [HttpGet]
        public IActionResult Login() {
			if (HttpContext.Session.GetInt32("SessionUserId") != null) {
				//If you get here α user is logged in
				return RedirectToAction("Index");
			}
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password) {

            var uID = await _loginService.AuthenticateUser(username, password);

            if (uID == -1) {
                // Wrong Credentials 
                // Display appropriate message
                // Redirect to Login
                Problem("Wrong credentials / User doesnt exist");
                return RedirectToAction("Login");
            }

            // Success Login
            // Update Session Info
            HttpContext.Session.SetInt32("SessionUserId", uID);

            var isAdmin = await _loginService.AuthenticateAdmin(uID);
            if (isAdmin) {
				// Update session info
				// Enable Admin functionality
				HttpContext.Session.SetInt32("SessionUserAdminRights", 1);    
            }else {
				HttpContext.Session.SetInt32("SessionUserAdminRights", 0);
			}
			return RedirectToAction("Index");
        }

        // GET: Home/Register
        // We have to check if user is already logged in in this session (Chesk using session),
        // * If yes -> Redirect to Index
        // * If no -> Redirect to Login
        [HttpGet]
        public IActionResult Register() {
            if (HttpContext.Session.GetInt32("SessionUserId") != null) {
                //If you get here α user is logged in
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Home/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Id, FullName, Username, Password, Email, ImageURLId, HasAdminPrivileges")] User user) {
            if (ModelState.IsValid) {
                _dbService.CreateUser(user);
            }
            // Setup session info while the user is created
            HttpContext.Session.SetInt32("SessionUserId", user.Id);
            HttpContext.Session.SetInt32("SessionUserAdminRights", user.HasAdminPrivileges ? 1 : 0);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
