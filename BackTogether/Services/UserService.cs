using BackTogether.Controllers;
using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackTogether.Services {
    public class UserService : IUser {

        private readonly BackTogetherContext _context;

        public UserService(BackTogetherContext context) {
            _context = context;
        }

        public async Task<User> CreateUser(User user) {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id) {
            var userToDelete = GetUser(id);
            if (userToDelete == null) {
                return false;
            } else {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
        } 

        public async Task<List<User>> GetAllUsers() {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public User? GetUser(int id) {
            if (id < 0) {
                return null;
            }
            var user = _context.Users.Include(u => u.ImageURL).FirstOrDefaultAsync(m => m.Id == id).Result;
            if (user == null) {
                return null;
            }
            return user;
        }

        public User? UpdateUser(int id) {
            throw new NotImplementedException();
        }
    }
}
