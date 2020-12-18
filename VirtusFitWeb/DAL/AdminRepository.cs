using BLL;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VirtusFitWeb.DAL
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminRepository(AppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<IdentityUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void ChangePassword(string email, string newPassword)
        {
            var user = GetUserByEmail(email);
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            user.PasswordHash = newPasswordHash;
            _context.Users.Update(user).Context.SaveChanges();
        }

        public void BlockUser(string email)
        {
            if (_context.Users.All(u => u.Email != email))
            {
                throw new InvalidDataException();
            }
            _context.BlockedUsers.Add(new BlockedUser {Email = email}).Context.SaveChanges();
        }

        public void DeleteUser(string email)
        {
            var user = GetUserByEmail(email);
            if (user == null)
            {
                throw new InvalidDataException();
            }

            _context.Users.Remove(user).Context.SaveChanges();
        }
    }
}