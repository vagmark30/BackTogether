using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Services {
    public class LoginService : ILogin {

        private readonly BackTogetherContext _context;

        public LoginService(BackTogetherContext context) {
            _context = context;
        }

        public Boolean AuthenticateAdmin(int id) {
            try {
                var user = GetUser(id);
                if (user.HasAdminPrivileges) {
                    return true;
                } else {
                    return false;
                }
            } catch (KeyNotFoundException) {
                return false;
            }
        }

        public int AuthenticateUser(string username, string password) {
            var user = _context.Users.FirstOrDefaultAsync(authUser => 
                authUser.Username == username && authUser.Password == password
            ).Result;

            if (user == null) {
                return -1;
            }
            return user.Id;
        }

        public User GetUser(int id) {
            var user = _context.Users.FirstOrDefaultAsync(user => user.Id == id).Result;
            if (user == null) {
                throw new KeyNotFoundException();
            }
            return user;
        }
    }
}
