using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public List<IdentityUser> ListAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public IdentityUser GetUserByEmail(string email)
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
