using BackTogether.Models;

namespace BackTogether.Services.api {
    public interface IUser {
        Boolean CheckUserValidation();
        Task<User> CreateUser(User user);
        Boolean RegisterUser();
        Boolean UpdateUser();
        Boolean DeleteUser();
        User? GetUser(int id);
        Boolean GetAllUsers();
    }
}
