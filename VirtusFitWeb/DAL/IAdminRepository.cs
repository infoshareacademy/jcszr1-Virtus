using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VirtusFitWeb.DAL
{
    public interface IAdminRepository
    {
        List<IdentityUser> GetAllUsers();
        void ChangePassword(string email, string newPassword);
        void BlockUser(string email);
        void UnblockUser(string email);
        void DeleteUser(string email);
    }
}