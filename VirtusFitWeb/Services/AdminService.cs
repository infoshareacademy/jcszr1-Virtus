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

        public void ChangePassword(string email, string newPassword)
        {
            _repository.ChangePassword(email,newPassword);
        }

        public void BlockUser(string email)
        {
            _repository.BlockUser(email);
        }

        public void RemoveUser(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
