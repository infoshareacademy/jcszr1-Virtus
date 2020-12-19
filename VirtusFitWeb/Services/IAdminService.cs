using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IAdminService
    {
        List<IdentityUser> ListAllUsers();
        void ChangePassword(string email, string newPassword);
        void BlockUser(string email);
        void UnblockUser(string email);
        void DeleteUser(string email);
    }
}
