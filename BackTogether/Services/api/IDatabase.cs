using BackTogether.Models;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Services.api {
    public interface IDatabase {

        /*  
         *  Create 
         */

                    // Users //
        public Task<User> CreateUser(User user);
                    // Projects //
        public Task<Project> CreateProject(Project project);

        /*  
         *  Read 
         */

                    // Users //
        public User? GetUserById(int id);
        public User? GetUserByProjectId(int projectId);
        public Task<List<User>> GetAllUsers();

                    // Projects //
        public Project GetProjectById(int id);
        public List<Project>? GetCreatedProjectsByUserId(int userId);
        public List<Project>? GetBackedProjectsByUserId(int userId);

        public Task<List<Project>> GetAllProjects(int amount);

        /* 
         * Update 
         */

                    // Users //
        public User UpdateUser(User user);
                    // Projects //
        public Project UpdateProject(Project project);

        /* 
         * Delete 
         */

                    // Users //
        public Task<bool> DeleteUser(int id);
                    // Projects //
        public Task<bool> DeleteProject(int id);
    }
}
