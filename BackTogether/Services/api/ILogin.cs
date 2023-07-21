using BackTogether.Models;

namespace BackTogether.Services.api {
    public interface ILogin {
        Task<int> AuthenticateUser(string username, string password);
        Task<bool> AuthenticateAdmin(int id);
    }
}
