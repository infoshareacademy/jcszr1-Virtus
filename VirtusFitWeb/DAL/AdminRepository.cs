using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public void RemoveUser(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}