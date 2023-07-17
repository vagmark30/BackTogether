using BackTogether.Models;

namespace BackTogether.Services.api {
    public interface ILogin {
        int AuthenticateUser(string username, string password);
        bool AuthenticateAdmin(int id);
    }
}
