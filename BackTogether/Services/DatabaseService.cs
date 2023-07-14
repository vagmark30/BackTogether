using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackTogether.Services {
    public class DatabaseService : IDatabase {

        private readonly BackTogetherContext _context;

        public DatabaseService(BackTogetherContext context) {
            _context = context;
        }

        /*  
         *  Create 
         */

                    // Users //
        public async Task<User> CreateUser(User user) {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

                    // Projects //
        public async Task<Project> CreateProject(Project project) {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        /*  
         *  Read 
         */

                    // Users //
        public User? GetUserById(int id) {
            var user = _context.Users.Include(u => u.ImageURL).FirstOrDefaultAsync(m => m.Id == id).Result;
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

            var user = (from n in _context.Users
                            where n.Id == userId
                            select n).Single();

            if (user == null) {
                return null;
            }
            return user;
        }
        public async Task<List<User>> GetAllUsers() {
            var users = await _context.Users.ToListAsync();
            return users;
        }

                    // Projects //
        public Project GetProjectById(int id) {
            throw new NotImplementedException();
        }
        // Get all projects created by User
        public List<Project>? GetCreatedProjectsByUserId(int userId) {
            var projects = (from n in _context.Projects
                            where n.UserId == userId
                            select n).ToList();

            if (projects == null) {
                return null;
            }
            return projects;
        }
        public List<Project>? GetBackedProjectsByUserId(int userId) {
            var projects = (from n in _context.Projects
                            where _context.Backings.Where(b => b.UserId == userId).Select(b => b.UserId).ToList().Contains(n.Id)
                            select n).ToList();

            if (projects == null) {
                return null;
            }
            return projects;
        }
        public async Task<List<Project>> GetAllProjects(int amount) {
            // Sorting by date created by default
            var projects = await _context.Projects.OrderByDescending(p => p.DateCreated).Take(amount).ToListAsync();
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
        public async Task<bool> DeleteUser(int id) {
            var userToDelete = GetUserById(id);
            if (userToDelete == null) {
                return false;
            } else {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
        }
                    // Projects //
        public async Task<bool> DeleteProject(int id) {
            var projectToDelete = GetProjectById(id);
            if (projectToDelete == null) {
                return false;
            } else {
                _context.Projects.Remove(projectToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
