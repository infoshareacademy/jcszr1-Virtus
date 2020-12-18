using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VirtusFitWeb.DAL
{
    public interface IAdminRepository
    {
        List<IdentityUser> GetAllUsers();
        IdentityUser GetUserByEmail(string email);
        void ChangePassword(string email, string newPassword);
        void BlockUser(string email);
        void RemoveUser(string email);
    }
}