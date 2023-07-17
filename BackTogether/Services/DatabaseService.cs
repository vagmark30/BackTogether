using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;

namespace BackTogether.Services {
    public class DatabaseService : IDatabase {

        private readonly BackTogetherContext _context;

        public DatabaseService(BackTogetherContext context) {
            _context = context;
        }

            // Users //
        public int CreateUser(User user) {
            _context.Add(user);
            return _context.SaveChanges();
        }

                    // Projects //
        public int CreateProject(Project project) {
            _context.Add(project);
            return _context.SaveChanges();
        }

        /*  
         *  Read 
         */

                    // Users //
        public User? GetUserById(int id) {
            var user = _context.Users.Include(u => u.ImageURL).FirstOrDefault(m => m.Id == id);
            if (user == null) {
                return null;
            }
            return user;
        }
        // Get creator of Project
        public User? GetUserByProjectId(int projectId) {
            var userId = (from n in _context.Projects
                          where n.Id == projectId
                          select n.UserId).Single();

            var user = GetUserById(userId);
            if (user == null) {
                return null;
            }

            return user;
        }
        public List<User> GetAllUsers() {
            return _context.Users.Include(u => u.ImageURL).ToList();
        }

                    // Projects //
        public Project? GetProjectById(int id) {
            var project = _context.Projects.Include(u => u.User).FirstOrDefault(m => m.Id == id);
            if (project == null) {
                return null;
            }
            return project;
        }
        // Get all projects created by User
        public List<Project>? GetCreatedProjectsByUserId(int userId) {
            var projects = (from n in _context.Projects.Include(u => u.User)
                            where n.UserId == userId
                            select n).ToList();
            if (projects == null) {
                return null;
            }
            return projects;
        }
        public List<Project>? GetBackedProjectsByUserId(int userId) {
            var projects = (from n in _context.Projects.Include(u => u.User)
                            where _context.Backings.Where(b => b.UserId == userId).Select(b => b.ProjectId).ToList().Contains(n.Id)
                            select n).ToList();
            if (projects == null) {
                return null;
            }
            return projects;
        }
        public List<Project> GetAllProjects(int amount) {
            // Sorting by date created by default
            var projects = _context.Projects.Include(u => u.User).OrderBy(n => n.DateCreated).Take(amount).ToList();
            return projects;
        }


        /* 
         * Update 
         */

        // Users //
        public User UpdateUser(User user) {
            throw new NotImplementedException();
        }
        // Projects
        public Project UpdateProject(Project project) {
            throw new NotImplementedException();
        }

        /* 
         * Delete 
         */

                    // Users //
        public bool DeleteUser(int id) {
            var userToDelete = GetUserById(id);
            if (userToDelete == null) {
                return false;
            } else {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }
        }
                    // Projects //
        public bool DeleteProject(int id) {
            var projectToDelete = GetProjectById(id);
            if (projectToDelete == null) {
                return false;
            } else {
                _context.Projects.Remove(projectToDelete);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
