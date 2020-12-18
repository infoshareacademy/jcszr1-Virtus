using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VirtusFitWeb.DAL
{
    public interface IAdminRepository
    {
        List<IdentityUser> GetAllUsers();
        IdentityUser GetUserByEmail();
        void ChangePassword(string email, string newPassword);
        void BlockUser(string email);
        void RemoveUser(string email);
    }
}