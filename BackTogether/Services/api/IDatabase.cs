using BackTogether.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Services.api {
    public interface IDatabase {

        //public bool IsAvailable();

        /*  
         *  Create 
         */

                    // Users //
        public int CreateUser(User user);
                    // Projects //
        public int CreateProject(Project project);

        /*  
         *  Read 
         */

                    // Users //
        public User? GetUserById(int? id);
        public User? GetUserByProjectId(int projectId);
        public List<User> GetAllUsers();

                    // Projects //
        public Project? GetProjectById(int id);
        public List<Project>? GetCreatedProjectsByUserId(int userId);
        public List<Project>? GetBackedProjectsByUserId(int userId);

        public List<Project> GetAllProjects(int amount);

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
        public bool DeleteUser(int id);
                    // Projects //
        public bool DeleteProject(int id);
    }
}
