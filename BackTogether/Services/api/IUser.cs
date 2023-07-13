using BackTogether.Models;

namespace BackTogether.Services.api {
    public interface IUser {
        Task<User> CreateUser(User user);
        User? UpdateUser(int id);
        Task<bool> DeleteUser(int id);
        User? GetUser(int id);
        Task<List<User>> GetAllUsers();
    }
}
