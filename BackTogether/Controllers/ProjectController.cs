using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;

namespace BackTogether.Controllers {
    public class ProjectController : Controller {
        private readonly IProject _projectService;

        public ProjectController(IProject projectService) {
            _projectService = projectService;
        }

        // GET: Project
        public IActionResult Index() {
            return RedirectToAction("Create");
        }

        // GET: Project/{id}
        public IActionResult Index(int id) {
            Project? project = _projectService.GetProject(id);
            if (project == null) {
                return View("Error");
            } else {
                // Show project page here
                return View();
            }
        }

        // GET: Project/Create
        // Check if user is logged in
        // * If yes -> Show project creation page
        // * If no -> redirect to login
        [HttpGet]
        public IActionResult Create() {
            // Show project creation page here
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,Category,UserId,CurrentFunding,FinalGoal,DateCreated")] Project project) {
            if (ModelState.IsValid) {
                _projectService.CreateProject(project);
            }
            return View(nameof(Index));
        }
    }
}
