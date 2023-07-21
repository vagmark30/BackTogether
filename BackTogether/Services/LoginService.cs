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

        public async Task<bool> AuthenticateAdmin(int id) {
            try {
                var user = await GetUser(id);
                if (user.HasAdminPrivileges) {
                    return true;
                } else {
                    return false;
                }
            } catch (KeyNotFoundException) {
                return false;
            }
        }

        public async Task<int> AuthenticateUser(string username, string password) {
            var user = await _context.Users.FirstOrDefaultAsync(authUser => 
                authUser.Username == username && authUser.Password == password
            );
            if (user == null) {
                return -1;
            }
            return user.Id;
        }

        private async Task<User> GetUser(int id) {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) {
                throw new KeyNotFoundException();
            }
            return user;
        }
    }
}
