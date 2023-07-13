using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Services {
    public class ProjectService : IProject {

        private readonly BackTogetherContext _context;

        public ProjectService(BackTogetherContext context) {
            _context = context;
        }

        public async Task<Project> CreateProject(Project project) {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProject(int id) {
            var projectToDelete = GetProject(id);
            if (projectToDelete == null) {
                return false;
            } else {
                _context.Projects.Remove(projectToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public List<Project> GetAllProjects(int amount) {
            return _context.Projects.Include(p => p.User).Include(p => p.ImageURLS).Include(p => p.Rewards).Take(amount).ToList();
        }

        public Project? GetProject(int id) {
            if (id < 0) {
                return null;
            }
            var project = _context.Projects.Include(p => p.User).Include(p => p.ImageURLS).Include(p => p.Rewards).FirstOrDefaultAsync(m => m.Id == id).Result;
            if (project == null) {
                return null;
            }
            return project;
        }

        public Task<Project> UpdateProject(int id) {
            throw new NotImplementedException();
        }
    }
}
