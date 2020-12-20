using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtusFitWeb.Services
{
    public interface IAdminService
    {
        List<IdentityUser> ListAllUsers();
        Task ChangePassword(string email, string newPassword);
        Task BlockUser(string email);
        Task UnblockUser(string email);
        Task DeleteUser(string email);
    }
}
