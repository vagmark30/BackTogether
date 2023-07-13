using BackTogether.Models;

namespace BackTogether.Services.api {
    public interface IProject {
        Task<Project> CreateProject(Project project);
        Project? GetProject(int id);
        Task<bool> DeleteProject(int id);
        Task<Project> UpdateProject(int id);
        List<Project> GetAllProjects(int amount);
    }
}
