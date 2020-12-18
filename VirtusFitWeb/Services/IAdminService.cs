using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IAdminService
    {
        List<IdentityUser> ListAllUsers();
        IdentityUser GetUserByEmail(string email);
        void ChangePassword(string email, string newPassword);
        void BlockUser(string email);
        void RemoveUser(string email);
    }
}
