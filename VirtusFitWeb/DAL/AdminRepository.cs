using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.DAL
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppContext _context;

        public AdminRepository(AppContext context)
        {
            _context = context;
        }
        public List<IdentityUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetUserByEmail()
        {
            throw new System.NotImplementedException();
        }

        public void ChangePassword(string email, string newPassword)
        {
            throw new System.NotImplementedException();
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