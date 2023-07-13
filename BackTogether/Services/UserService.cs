using BackTogether.Controllers;
using BackTogether.Data;
using BackTogether.Models;
using BackTogether.Services.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Services {
    public class UserService : IUser{

        private readonly BackTogetherContext _context;

        public UserService(BackTogetherContext context) {
            _context = context;
        }

        public bool CheckUserValidation() {
            throw new NotImplementedException();
        }

        public async Task<User> CreateUser(User user) {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool DeleteUser() {
            throw new NotImplementedException();
        } 

        public bool GetAllUsers() {
            throw new NotImplementedException();
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

        public bool RegisterUser() {
            throw new NotImplementedException();
        }

        public bool UpdateUser() {
            throw new NotImplementedException();
        }
    }
}
